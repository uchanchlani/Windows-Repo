#A Kinect 2.0 data **synchronization tool**

##What this tool does?
* It collects all the Kinect streams from a particular xef file, which are to be synchronized
* It uses all these streams to synchronize them to their respective .MAT files.

##PC requirements
* Windows 8 or above
* Visual studio 13 or above
* Kinect studio 2.0
* Matlab

##Preprocessing
* To make this run for the first time, when you download it, you need to go to the project files in the *gui_workables* folder and build the solutions for all of them seperately in the Visual Studio

##How to run it
* Run the batch file. It will ask you which stream you need to collect. Collect all the streams separately, in the paths required
* Finally run the synchronizer. it actually runs the matlab script in the **Matlab**. If it doesn't works from the batch file, the script can be run separately in the Matlab. You can refer the [MatLab Website](http://in.mathworks.com/help/matlab/) for the same.

##Points to remember:
* Skeleton frames, can be extracted all at once.
* For all the remaining frames extract not more than 150 frames **strictly**.
* Say the working directory for the frames is x. 5 folders are needed to be made manually in the directory x namely:
	* x\depth_data: For the depth frames
	* x\color_data: For the color frames
	* x\skeleton_data: For the skeleton frames
	* x\bodyindex_data: For the Body Index frames
	* x\synchronized_data: For the synchronized frames
* For all the 4 frame collecting listener guis, their respective directories are provided, i.e. for depth provide *x\depth_data*, for color provide *x\color_data*
* For synchronizations, provide the master directory, i.e. *x*
