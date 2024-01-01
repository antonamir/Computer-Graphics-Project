using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

namespace Graphics
{
    class Renderer
    {
        Shader sh;
        uint vertexBufferID;
        public void Initialize()
        {
            // Read the sheders 
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");
           
           // determine background color 
            Gl.glClearColor(1, 1, 0.99f, 1);

            // the points of snake ;;; initialize vertex data array

            float[] points =
             {          
                // FIRST FAN
                        -0.596f, -0.24f, 0.0f,  // 4
                        -0.022f, -0.05f, 0.0f,  // 8
                        -0.574f, 0.33f, 0.0f,  // 7
                        -0.809f, -0.061f, 0.0f,  // 6
                        -0.882f, -0.408f, 0.0f,  // 3
                        -0.882f, -0.922f, 0.0f,  // 1
                        -0.346f, -0.922f, 0.0f,  // 2
                        -0.279f, -0.274f, 0.0f,  // 5
                        -0.022f, -0.05f, 0.0f,  // 8



                        
                        0.066f, 0.274f, 0.0f,  // 10
                        -0.096f, 0.654f, 0.0f,  // 9
                        -0.574f, 0.33f, 0.0f,  // 7
                        -0.022f, -0.05f, 0.0f,  // 8
                        0.272f, 0.084f, 0.0f,  // 11
                         0.066f, 0.274f, 0.0f,  // 10


                          // EYES
                        0.118f, 0.419f, 0.0f,  // 18
                        0.154f, 0.497f, 0.0f, // 17
                        0.147f, 0.553f, 0.0f,  // 16
                        0.096f, 0.598f, 0.0f,  // 15
                        0.037f, 0.553f, 0.0f,  // 14
                        0.015f, 0.486f, 0.0f,  // 13
                        0.059f, 0.408f, 0.0f,  // 12
                        
                        // THE HIGHER PART OF THE HEAD 
                           -0.096f, 0.654f, 0.0f,  // 9
                            0.066f, 0.274f, 0.0f,  // 10
                            0.206f, 0.453f, 0.0f,  // 20
                            0.265f, 0.687f, 0.0f,  // 19
                       
                       // THE TRIANGLE 1
                        0.265f, 0.687f, 0.0f,  // 19
                        0.206f, 0.453f, 0.0f,  // 20
                        0.449f, 0.575f, 0.0f,  // 26

                        // THEباقي الوش  
                        0.449f, 0.575f, 0.0f,  // 26
                        0.206f, 0.453f, 0.0f,  // 20
                        0.066f, 0.274f, 0.0f,  // 10
                        0.272f, 0.084f, 0.0f , // 11
                        0.449f, 0.33f, 0.0f , // 22
                        0.434f, 0.374f, 0.0f,  // 23
                        

                        // THE  MOUSE IS LINE 
                        0.257f, 0.318f, 0.0f,  // 21
                        0.434f, 0.374f, 0.0f,  // 23


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



                       // // بديل الجزء المصمط
                       // -0.882f, -0.922f, 0.0f,  // 1
                       // -0.346f, -0.922f, 0.0f,  // 2
                       // -0.596f, -0.24f, 0.0f,  // 4
                       // -0.882f, -0.922f, 0.0f,  // 1
                       // -0.596f, -0.24f, 0.0f,  // 4
                       // -0.279f, -0.274f, 0.0f,  // 3
                       // -0.279f, -0.274f, 0.0f,  // 3
                       // -0.596f, -0.24f, 0.0f,  // 4
                       // -0.809f, -0.061f, 0.0f,  // 6
                       // -0.809f, -0.061f, 0.0f,  // 6
                       // -0.596f, -0.24f, 0.0f,  // 4
                       // -0.574f, 0.33f, 0.0f,  // 7
                       // -0.596f, -0.24f, 0.0f,  // 4
                       // -0.022f, -0.05f, 0.0f,  // 8
                       // -0.574f, 0.33f, 0.0f,  // 7
                       // -0.022f, -0.05f, 0.0f,  // 8
                       // 0.272f, 0.084f, 0.0f,  // 11
                       // 0.066f, 0.274f, 0.0f,  // 10
                       // -0.022f, -0.05f, 0.0f,  // 8
                       // 0.066f, 0.274f, 0.0f,  // 10
                       // -0.574f, 0.33f, 0.0f,  // 7
                       // 0.066f, 0.274f, 0.0f,  // 10
                       // -0.022f, -0.05f, 0.0f,  // 8

                       ////-0.022f, -0.05f, 0.0f,  // 8
                       ////-0.096f, 0.654f, 0.0f , // 9
                       //// 0.066f, 0.274f, 0.0f,  // 10
                       //// 0.272f, 0.084f, 0.0f,  // 11
                       //// 0.449f, 0.33f, 0.0f , // 22
                       //// 0.434f, 0.374f, 0.0f,  // 23
                       //// 0.449f, 0.575f, 0.0f,  // 26
                       //// 0.265f, 0.687f, 0.0f,  // 19

                      


                       //     0.449f, 0.33f, 0.0f,  // 22
                       //     0.434f, 0.374f, 0.0f,  // 23
                       //     0.404f, 0.408f, 0.0f,  // 24
                       //     0.419f, 0.464f, 0.0f,  // 25
                       //     0.449f, 0.575f, 0.0f,  // 26
                       //     0.61f, 0.352f, 0.0f,  // 27
                       //     0.596f, 0.453f, 0.0f,  // 28
                       //     0.728f, 0.441f, 0.0f,  // 29
                       //     0.772f, 0.443f, 0.0f,  // 30
                       //     0.735f, 0.553f, 0.0f,  // 31
                       //     0.919f, 0.486f, 0.0f,  // 32
                       //     0.853f, 0.564f, 0.0f,  // 33
                       //     0.963f, 0.777f, 0.0f,  // 34
                       //     0.875f, 0.732f, 0.0f,  // 35
                       //     0.824f, 0.944f, 0.0f,  // 36
                       //     0.853f, 0.844f, 0.0f,  // 37
                       //     0.779f, 0.844f, 0.0f  // 38
                        




                      //// 0
                      //-0.574f, -0.253f, 0,
                      ////1
                      //  -0.566f, -0.333f, 0,
                      ////2
                      // -0.794f, -0.046f, 0
                      ////3
                      //-0.86f, -0.437f, 0
                      // // 4
                      //-0.86f, -0.943f, 0,
                      ////5
                      //  -0.324f, -0.943f, 0,
                      ////6
                      // -0.257f, -0.276f, 0
                      ////7
                      //-0.015f, -0.057f, 0


            };

            //generate buffer to translate from ram to video ram
            vertexBufferID=GPU.GenerateBuffer(points);
        }

        public void Draw()
        {
            // clear the color buffer
            
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            sh.UseShader();
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 0, IntPtr.Zero);

            //// Draw your primitives !
            //// 1-  Eyes        
            Gl.glPointSize(10);
            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 0, 9);
            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 9, 5);
            //Gl.glDrawArrays(Gl.GL_LINE_LOOP, 15, 7) ;
            Gl.glDrawArrays(Gl.GL_POLYGON, 15, 7);
            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 22,4);
            //Gl.glDrawArrays(Gl.GL_LINE_LOOP, 22, 4);
            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 26, 3);
            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 29, 6);
            Gl.glDrawArrays(Gl.GL_LINES, 35, 2);
            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 37, 15);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 52, 15);
            //Gl.glDrawArrays(Gl.GL_LINE_STRIP, 67, 3);
            //Gl.glDrawArrays(Gl.GL_LINE_STRIP, 70, 3);
            //Gl.glDrawArrays(Gl.GL_LINE_STRIP, 73, 3);
            //Gl.glDrawArrays(Gl.GL_LINE_STRIP, 76, 3);
            //Gl.glDrawArrays(Gl.GL_LINE_STRIP, 79, 3);
            //Gl.glDrawArrays(Gl.GL_LINE_STRIP, 82, 3);
            //Gl.glDrawArrays(Gl.GL_LINE_STRIP, 85, 3);
            //Gl.glDrawArrays(Gl.GL_LINE_STRIP, 88, 3);
            //Gl.glDrawArrays(Gl.GL_LINE_STRIP, 91, 3);
            //Gl.glDrawArrays(Gl.GL_LINE_STRIP, 94, 3);


            // Gl.glDrawArrays(Gl.GL_TRIANGLES, 3, 6);
            //// 2 - body

            //// 3 - Arms
            //glDrawArrays();
            //// 4 - Fan
            //glDrawArrays();
            //// 5 - legs
            //glDrawArrays();	
            //// 6 - Nose 
            //glDrawArrays();
            //// 7 - Mouth
            //glDrawArrays();


            Gl.glDisableVertexAttribArray(0);



        }
        public void Update()
        {

        }
        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}
