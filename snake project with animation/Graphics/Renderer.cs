using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

//include GLM library
using GlmNet;


using System.IO;
using System.Diagnostics;

namespace Graphics
{
    class Renderer
    {
        Shader sh;
        
        uint triangleBufferID;
        uint xyzAxesBufferID;

        //3D Drawing
        mat4 ModelMatrix;
        mat4 ViewMatrix;
        mat4 ProjectionMatrix;
        
        int ShaderModelMatrixID;
        int ShaderViewMatrixID;
        int ShaderProjectionMatrixID;

        const float rotationSpeed = 1f;
        float rotationAngle = 0;

        public float translationX=0, 
                     translationY=0, 
                     translationZ=0;

        Stopwatch timer = Stopwatch.StartNew();

        vec3 triangleCenter;

        public void Initialize()
        {
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");

            Gl.glClearColor(0, 0, 0.4f, 1);
            
            float[] triangleVertices= { 
		        // FIRST FAN
                        -0.596f, -0.24f, 0.0f,  // 4
                        1,0,0,
                        -0.022f, -0.05f, 0.0f,  // 8
                        1,0,0,
                        -0.574f, 0.33f, 0.0f,  // 7
                        1,0,0,
                        -0.809f, -0.061f, 0.0f,  // 6
                        1,0,0,
                        -0.882f, -0.408f, 0.0f,  // 3
                        1,0,0,
                        -0.882f, -0.922f, 0.0f,  // 1
                        1,0,0,
                        -0.346f, -0.922f, 0.0f,  // 2
                        1,0,0,
                        -0.279f, -0.274f, 0.0f,  // 5
                        1,0,0,
                        -0.022f, -0.05f, 0.0f,  // 8
                        1,0,0,



                        0.066f, 0.274f, 0.0f,  // 10
                        1,0,0,
                        -0.096f, 0.654f, 0.0f,  // 9
                        1,0,0,
                        -0.574f, 0.33f, 0.0f,  // 7
                        1,0,0,
                        -0.022f, -0.05f, 0.0f,  // 8
                        1,0,0,
                        0.272f, 0.084f, 0.0f,  // 11
                        1,0,0,
                         0.066f, 0.274f, 0.0f,  // 10
                         1,0,0,


                          // EYES
                        0.118f, 0.419f, 0.0f,  // 18
                        0,0,1,
                        0.154f, 0.497f, 0.0f, // 17
                        0,0,1,
                        0.147f, 0.553f, 0.0f,  // 16
                        0,0,1,
                        0.096f, 0.598f, 0.0f,  // 15
                        0,0,1,
                        0.037f, 0.553f, 0.0f,  // 14
                        0,0,1,
                        0.015f, 0.486f, 0.0f,  // 13
                        0,0,1,
                        0.059f, 0.408f, 0.0f,  // 12
                        0,0,1,
                        
                        // THE HIGHER PART OF THE HEAD 
                           -0.096f, 0.654f, 0.0f,  // 9
                           1,0,0,

                            0.066f, 0.274f, 0.0f,  // 10
                            1,0,0,
                            0.206f, 0.453f, 0.0f,  // 20
                            1,0,0,
                            0.265f, 0.687f, 0.0f,  // 19
                            1,0,0,
                       
                       // THE TRIANGLE 1
                        0.265f, 0.687f, 0.0f,  // 19
                        1,0,0,
                        0.206f, 0.453f, 0.0f,  // 20
                        1,0,0,
                        0.449f, 0.575f, 0.0f,  // 26
                        1,0,0,

                        // THEباقي الوش  
                        0.449f, 0.575f, 0.0f,  // 26
                        1,0,0,
                        0.206f, 0.453f, 0.0f,  // 20
                        1,0,0,
                        0.066f, 0.274f, 0.0f,  // 10
                        1,0,0,
                        0.272f, 0.084f, 0.0f , // 11
                        1,0,0,
                        0.449f, 0.33f, 0.0f , // 22
                        1,0,0,
                        0.434f, 0.374f, 0.0f,  // 23
                        1,0,0,
                        

                        // THE  MOUSE IS LINE 
                        0.257f, 0.318f, 0.0f,  // 21
                        0,0,0,
                        0.434f, 0.374f, 0.0f,  // 23
                        0,0,0,

                        //  اللسان 
                        0.434f, 0.374f, 0.0f,  // 23
                        0.61f, 0.352f, 0.0f,  // 27
                        0.728f, 0.441f, 0.0f,  // 29
                        0.772f, 0.443f, 0.0f,  // 30
                        0.919f, 0.486f, 0.0f,  // 32
                        0.963f, 0.777f, 0.0f,  // 34
                        0.824f, 0.944f, 0.0f,  // 36
                        0.853f, 0.844f, 0.0f,  // 37
                        0.779f, 0.844f, 0.0f,  // 38
                        0.875f, 0.732f, 0.0f,  // 35
                        0.853f, 0.564f, 0.0f,  // 33
                        0.735f, 0.553f, 0.0f,  // 31
                        0.596f, 0.453f, 0.0f,  // 28
                        0.440f, 0.464f, 0.0f,  // 25
                        0.434f, 0.408f, 0.0f,  // 24
                        0.434f, 0.374f, 0.0f,  // 23

                       
                        //  تخطيط اللسان
                        0.434f, 0.374f, 0.0f,  // 23
                        0.596f, 0.453f, 0.0f,  // 28
                        0.596f, 0.453f, 0.0f,  // 28
                        0.728f, 0.441f, 0.0f,  // 29
                        0.772f, 0.443f, 0.0f,  // 30
                        0.853f, 0.564f, 0.0f,  // 33
                        0.853f, 0.564f, 0.0f,  // 33
                        0.919f, 0.486f, 0.0f,  // 32
                        0.919f, 0.486f, 0.0f,  // 32
                        0.875f, 0.732f, 0.0f,  // 35
                        0.875f, 0.732f, 0.0f,  // 35
                        0.963f, 0.777f, 0.0f,  // 34
                        0.963f, 0.777f, 0.0f,  // 34
                        0.853f, 0.844f, 0.0f  // 37
            }; // Triangle Center = (10, 7, -5)
            
            triangleCenter = new vec3(10, 7,-5);

            float[] xyzAxesVertices = {
		        //x
		        0.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f, 
		        100.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f, 
		        //y
	            0.0f, 0.0f, 0.0f,
                0.0f,1.0f, 0.0f, 
		        0.0f, 100.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 
		        //z
	            0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f,  
		        0.0f, 0.0f, -100.0f,
                0.0f, 0.0f, 1.0f,  
            };


            triangleBufferID = GPU.GenerateBuffer(triangleVertices);
            xyzAxesBufferID = GPU.GenerateBuffer(xyzAxesVertices);

            // View matrix 
            ViewMatrix = glm.lookAt(
                        new vec3(1, 1, 1), // Camera is at (0,5,5), in World Space
                        new vec3(0, 0, 0), // and looks at the origin
                        new vec3(0, 1, 0)  // Head is up (set to 0,-1,0 to look upside-down)
                );
            // Model Matrix Initialization
            ModelMatrix = new mat4(1);

            //ProjectionMatrix = glm.perspective(FOV, Width / Height, Near, Far);
            ProjectionMatrix = glm.perspective(45.0f, 4.0f / 3.0f, 0.1f, 100.0f);
            
            // Our MVP matrix which is a multiplication of our 3 matrices 
            sh.UseShader();


            //Get a handle for our "MVP" uniform (the holder we created in the vertex shader)
            ShaderModelMatrixID = Gl.glGetUniformLocation(sh.ID, "modelMatrix");
            ShaderViewMatrixID = Gl.glGetUniformLocation(sh.ID, "viewMatrix");
            ShaderProjectionMatrixID = Gl.glGetUniformLocation(sh.ID, "projectionMatrix");

            Gl.glUniformMatrix4fv(ShaderViewMatrixID, 1, Gl.GL_FALSE, ViewMatrix.to_array());
            Gl.glUniformMatrix4fv(ShaderProjectionMatrixID, 1, Gl.GL_FALSE, ProjectionMatrix.to_array());

            timer.Start();
        }

        public void Draw()
        {
            sh.UseShader();
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            #region XYZ axis

            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, xyzAxesBufferID);
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, new mat4(1).to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glEnableVertexAttribArray(1);

            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
             
            Gl.glDrawArrays(Gl.GL_LINES, 0, 6);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion

            #region Animated Triangle
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, triangleBufferID);
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());

            Gl.glEnableVertexAttribArray(0);
            Gl.glEnableVertexAttribArray(1);

            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            // Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 3);
            // the below part of the of snake
            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 0, 9);
            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 9, 5);
            ////Gl.glDrawArrays(Gl.GL_LINE_LOOP, 15, 7) ;


            ///eye
            Gl.glDrawArrays(Gl.GL_POLYGON, 15, 7);
            // the head
            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 22, 4);
            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 26, 3);
            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 29, 6);

            // tongue
            Gl.glDrawArrays(Gl.GL_LINES, 35, 2);
            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 37, 15);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 52, 15);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion
        }
        

        public void Update()
        {

            timer.Stop();
            var deltaTime = timer.ElapsedMilliseconds/1000.0f;

            rotationAngle += deltaTime * rotationSpeed;

            List<mat4> transformations = new List<mat4>();
           // transformations.Add(glm.translate(new mat4(1), -1 * triangleCenter));
            transformations.Add(glm.rotate(rotationAngle, new vec3(0, 1, 0)));
          //  transformations.Add(glm.translate(new mat4(1),  triangleCenter));
            transformations.Add(glm.translate(new mat4(1), new vec3(translationX, translationY, translationZ)));

            ModelMatrix =  MathHelper.MultiplyMatrices(transformations);
            
            timer.Reset();
            timer.Start();
        }
        
        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}
