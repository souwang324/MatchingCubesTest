using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace MatchingCubesTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int mode = 2;
            if (mode == 1)
                MatchCubeTest1();
            else
                MatchCubeTest2();
        }

        public static void MatchCubeTest1()
        {
            vtkDICOMImageReader dcmReader = vtkDICOMImageReader.New();
            dcmReader.SetDirectoryName("../../../../res/CT");
            dcmReader.Update();

            int[] bands = new int[6];
            bands = dcmReader.GetOutput().GetExtent();


            vtkMarchingCubes pMatchingCube = vtkMarchingCubes.New();
            pMatchingCube.SetInputConnection(dcmReader.GetOutputPort());
            pMatchingCube.SetValue(0, -500); // iso value
            pMatchingCube.ComputeScalarsOff();
            pMatchingCube.ComputeNormalsOn();
            pMatchingCube.Update();

            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(pMatchingCube.GetOutputPort());

            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);
            double[] color = new double[3] { 240 / 255.0, 184 / 255.0, 160 / 255.0 };
            actor.GetProperty().SetColor(color[0], color[1], color[2]);
            vtkProperty back_prop = vtkProperty.New();
            double[] color1 = new double[3] { 255 / 255.0, 229 / 255.0, 200 / 255.0 };
            back_prop.SetDiffuseColor(color1[0], color1[1], color1[2]);
            actor.SetBackfaceProperty(back_prop);

            vtkRenderer renderer = vtkRenderer.New();
            renderer.AddActor(actor);
            renderer.SetBackground(.1, .2, .3);
            renderer.ResetCamera();

            vtkRenderWindow renderWin = vtkRenderWindow.New();
            renderWin.AddRenderer(renderer);

            vtkRenderWindowInteractor interactor = vtkRenderWindowInteractor.New();
            interactor.SetRenderWindow(renderWin);

            renderWin.Render();
            interactor.Start();

        }

        public static void MatchCubeTest2()
        {
            vtkDICOMImageReader dcmReader = vtkDICOMImageReader.New();
            dcmReader.SetDirectoryName("../../../../res/CT");
            dcmReader.Update();

            int[] bands = new int[6];
            bands = dcmReader.GetOutput().GetExtent();


            vtkMarchingCubes pMatchingCube = vtkMarchingCubes.New();
            pMatchingCube.SetInputConnection(dcmReader.GetOutputPort());
            pMatchingCube.SetValue(0, 330); // iso value
            pMatchingCube.ComputeScalarsOff();
            pMatchingCube.ComputeNormalsOn();
            pMatchingCube.Update();

            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(pMatchingCube.GetOutputPort());

            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);

            vtkRenderer renderer = vtkRenderer.New();
            renderer.AddActor(actor);
            renderer.SetBackground(.1, .2, .3);
            renderer.ResetCamera();

            vtkRenderWindow renderWin = vtkRenderWindow.New();
            renderWin.AddRenderer(renderer);

            vtkRenderWindowInteractor interactor = vtkRenderWindowInteractor.New();
            interactor.SetRenderWindow(renderWin);

            renderWin.Render();
            interactor.Start();

        }

    }
}
