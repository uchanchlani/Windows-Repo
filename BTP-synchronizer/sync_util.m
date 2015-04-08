clearvars;
path = 'G:\01\';
depthtimearr = load(strcat(path,'depth_data\Depth_Timings.txt'));
colortimearr = load(strcat(path,'color_data\Color_Timings.txt'));
depthtime = 0.0;
colortime = 0.0;
jmin = 50;
jmax = 60;
i = 1;
j = 1;
max = 543;
k = 1;
while i <= max && j <= max
    depthtime = depthtime + depthtimearr(i);
    colortime = colortime + colortimearr(j);
    depthtimesec = depthtime / 15000;
    colortimesec = colortime / 15000;
    delta = depthtime - colortime;
    if abs(delta) > 750
        if delta > 0
            j = j + 1;
            depthtime = depthtime - depthtimearr(i);
        else
            i = i + 1;
            colortime = colortime - colortimearr(j);
        end
    else
        if (i >= jmin) && (i <= jmax)
        	dpath = strcat(path,'depth_data\Depthframe',int2str(i),'.MAT');
                depth = load(dpath);
            depth = depth.depthmat;
            depth = rot90(depth,3);
            cpath = strcat(path,'color_data\color_image',int2str(j),'.jpg');
            color = imread(cpath);
            spath = strcat(path,'skeleton_data\Skeletonframe',int2str((i+j)/2),'.txt');
            skeleton = load(spath);
            bpath = strcat(path,'bodyindex_data\bodyIndex',int2str((i+j)/2),'.png');
            bodyIndex = imread(bpath);
            savepath = strcat(path,'synchronized_data\sync',int2str(k),'.MAT');
            save(savepath,'depth','color','skeleton','bodyIndex','depthtimesec','colortimesec');
        end
        i = i + 1;
        j = j + 1;
        k = k + 1;
    end
end
k