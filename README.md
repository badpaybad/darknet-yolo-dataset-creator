# yolo-dataset-creator
yolo-dataset-creator Help you to labled photo and auto generate file(s) to training you model by YOLO CNN
![ui](https://github.com/badpaybad/yolo-dataset-creator/blob/main/ui.PNG)

## target
Check folder we want this result
				/result/yolodataset 

You want to do this cmd:
				"C:\robot\yolo-dataset-creator\RobotCloud.YoloCreatorDataTrain\bin\Debug\darknet/darknet.exe" detector train C:\robot\yolo-dataset-creator\RobotCloud.YoloCreatorDataTrain\bin\Debug\yolodataset\obj.data C:\robot\yolo-dataset-creator\RobotCloud.YoloCreatorDataTrain\bin\Debug\yolodataset\yolo-obj.cfg C:\robot\yolo-dataset-creator\RobotCloud.YoloCreatorDataTrain\bin\Debug\darknet/darknet19_448.conv.23

## UI guider
				Load img, draw box, then save to collection (folder)

				Open collection , create Yolo dataset

				Auto generate: obj.data, obj.names, train.txt, yolo-obj.cfg

## Windowform run in win 10
dotnet framework 4.7.2

Visual studio 2019

#### To create darknet.exe https://github.com/AlexeyAB/darknet

#### https://github.com/microsoft/vcpkg

## cuda 10.1.x
https://wish-aks.medium.com/installing-cuda-and-cudnn-on-windows-10-f735585159f7
https://www.reddit.com/r/nvidia/comments/hg45ux/is_cuda_11_compatible_with_tensorflow/
https://github.com/AlexeyAB/darknet/issues/7081 cudnn 8+ ; cuda 11

PS Code\>              git clone https://github.com/microsoft/vcpkg
PS Code\>              cd vcpkg
PS Code\vcpkg>         $env:VCPKG_ROOT=$PWD
PS Code\vcpkg>         .\bootstrap-vcpkg.bat 
PS Code\vcpkg>         .\vcpkg install darknet[opencv-base,cuda,cudnn]:x64-windows #replace "full" with "opencv-base,cuda,cudnn"
PS Code\vcpkg>         cd ..

## Yolo rule create data train
				Img will be labled in the same folder with notation labled.
				eg: c:/test/img.jpg
Inside notation file
				File will be: c:/test/img.txt
				The content: 
				lable center-relateive-x center-relative-y relative-width relative-height
				
				du 0.295 0.1025 0.00625 0.0075

				https://vovaprivalov.medium.com/training-alekseyab-yolov3-on-own-dataset-in-google-colab-8f3de8105d86

## Yolo rule .cfg 
For PoC purpose

				[convolutional]
				size=1
				stride=1
				pad=1
				filters=40 # important (classes + coords +1)*num
				activation=linear

				[region]
				anchors = 1.08,1.19,  3.42,4.41,  6.63,11.38,  9.42,5.11,  16.62,10.52
				bias_match=1
				classes=3 # classes (the number of labled)
				coords=4
				num=5
				softmax=1
				jitter=.2
				rescore=1

## Yolo pre train model 

darknet19_448.conv.23

https://github.com/shimat/opencvsharp_samples/blob/12d49782d3480fcddc5a737d2fa1fbbaaf324d3f/SamplesCS/Samples/CaffeSample.cs