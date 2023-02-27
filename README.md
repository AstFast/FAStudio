# FA Studio

@Chinese

English 

## 中文

### FAStudio

用于解压植物大战僵尸部分游戏资源文件，但这个仅针对植物大战僵尸有用

## 支持的格式

###### XNB：

        XNB<=>Wav

        XNB<=>Font

        XNB=>PNG

        WMA`生成`XNB

###### EA ADPCM：

        SPS/SNS=>Wav

## 使用事例及注意事项

/以后的版本会优化这些/

##### Font：

###### XNB=>Font

Input：C:\Temp\SA.xnb

*仅提供Input完整路径即可，文件会生成在Input同目录

##### Font=>XNB

Input：C:\Temp\SA.json或者C:\Temp\SA.png

提供json或png任意一个完整目录即可，程序会找同名的另一个文件

##### XNB<=>PNG Or WMA Or WAV

Input：C:\Temp\SA.png

Output:C:\Temp\SA.xnb

*需要提供完整文件路径(精准到文件名称)(EA=>WAV同样)

### 需要环境

(应该是)net7及以上

### 未完成的任务

###### 1. 完成对SPS/SNS的编码(SPS/SNS是EA-ADPCM XAS)

###### 2. 完成对PNG=>XNB(是的，目前程序里的是不正确的)

###### 3.优化界面

### Author

by     SFDA-冬

或者叫（冬日-春上）

### 推荐项目

我自己用python写的:

[ An early project](https://github.com/AstFast/FA-toolbox)

其他人写的

[GitHub - A project to convert many kinds of files used in PopCap Games.](https://github.com/YingFengTingYu/PopStudio)

EA-ADPCM：

[GitHub - CrabJournal/EA-ADPCM-Codec](https://github.com/CrabJournal/EA-ADPCM-Codec)

## Table

| 类型    | 文件  | 是或否 | 目标文件         | 类型    | 文件           | 是或否         | 目标文件 |
| ----- | --- | --- | ------------ | ----- | ------------ | ----------- | ---- |
| Image | XNB | Yes | PNG          | Image | PNG          | Problematic | XNB  |
| Font  | XNB | YES | JSON And PNG | Font  | JSON And PNG | YES         | XNB  |
| Sound | XNB | YES | WAV          | Sound | WAV          | YES         | XNB  |
|       |     |     |              | Music | WMA          | YES         | XNB  |
|       |     |     |              |       |              |             |      |
