@echo off
echo "What do you want to do?"
echo "Press 1 for getting Depth data"
echo "2 for Color"
echo "3 for Body Index"
echo "4 for skeleton data"
echo "5 for synching the files"
set /p id="Enter your choice: "
echo %id%
IF %id%==1 (
	start /d "%CD%\gui_workables\DepthStreams\KinectMLConnect\bin\Debug" KinectMLConnect.exe
) ELSE IF %id%==2 (
	start /d "%CD%\gui_workables\ColorStreams\KinectMLConnect\bin\Debug" KinectMLConnect.exe
) ELSE IF %id%==3 (
	start /d "%CD%\gui_workables\BodyIndexBasics-WPF\bin\AnyCPU\Debug" BodyIndexBasics-WPF.exe
) ELSE IF %id%==4 (
	start /d "%CD%\gui_workables\KinectStreams\KinectStreams\bin\Debug" KinectStreams.exe
) ELSE IF %id%==5 (
	"C:\Program Files\MATLAB\R2012a\bin\matlab.exe" -nodisplay -nosplash -nodesktop -r run('%CD%\sync_to_mat.m')
)
pause