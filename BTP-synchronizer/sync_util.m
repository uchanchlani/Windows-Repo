clearvars;
path = 'G:\01\';
depthtimearr = load(strcat(path,'depth_data\Depth_Timings.txt'));
colortimearr = load(strcat(path,'color_data\Color_Timings.txt'));
depthtime = 0.0;
colortime = 0.0;
jmin = 50;
jmax = 60;
for i = 1:543
    depthtime = depthtime + depthtimearr(i);
    colortime = colortime + colortimearr(i);
    depthtimesec = depthtime / 1000;
    colortimesec = colortime / 1000;
    if (i >= jmin) && (i <= jmax)
    	dpath = strcat(path,'depth_data\Depthframe',int2str(i),'.MAT');
        depth = load(dpath);
        depth = depth.depthmat;
        depth = rot90(depth,3);
        cpath = strcat(path,'color_data\color_image',int2str(i),'.jpg');
        color = imread(cpath);
        spath = strcat(path,'skeleton_data\Skeletonframe',int2str(i),'.txt');
        skeleton = load(spath);
        bpath = strcat(path,'bodyindex_data\bodyIndex',int2str(i),'.png');
        bodyIndex = imread(bpath);
        savepath = strcat(path,'synchronized_data\sync',int2str(i),'.MAT');
        save(savepath,'depth','color','skeleton','bodyIndex','depthtimesec','colortimesec');
    end
end