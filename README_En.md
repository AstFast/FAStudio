# FA Studio

Chinese

@English

## English

Used to decompress some game resource files of Plants VS Zombies, but this is only useful for Plants VS Zombies.

### Supported formats

XNB：

        XNB<=>Wav

        XNB<=>Font

        XNB=>PNG

        WMA `create` XNB

###### EA ADPCM：

        SPS/SNS=>Wav

### Use Cases and Precautions

/Future versions will optimize these/

Font：

###### XNB=>Font

Input：C:\Temp\SA.xnb

*Only provide the full path of Input, and the file will be generated in the same directory as Input

##### Font=>XNB

Input：C:\Temp\SA.json或者C:\Temp\SA.png

*Provide any complete directory of json or png, and the program will find another file with the same name

##### XNB<=>PNG Or WMA Or WAV

Input：C:\Temp\SA.png

Output:C:\Temp\SA.xnb

*The complete file path (accurate to the file name) needs to be provided (EA=>WAV is the same)

### Use environment

(Should be) net7 and above

### Unfinished tasks

1.Complete the encoding of SPS/SNS (SPS/SNS is EA-ADPCM XAS)

2.Complete PNG=>XNB (yes, the current program is incorrect)

3.Optimize the interface

### Author

by     SFDA-冬

Or（冬日-春上）

### Recommendable projects

I wrote it myself in python:

[An early project](https://github.com/AstFast/FA-toolbox)

Written by others

[GitHub - A project to convert many kinds of files used in PopCap Games.](https://github.com/YingFengTingYu/PopStudio)

EA-ADPCM：

[GitHub - CrabJournal/EA-ADPCM-Codec](https://github.com/CrabJournal/EA-ADPCM-Codec)

### Table

| Type  | File | Yes or No | Target File  | Type  | File         | Yes or No   | Target File |
| ----- | ---- | --------- | ------------ | ----- | ------------ | ----------- | ----------- |
| Image | XNB  | Yes       | PNG          | Image | PNG          | Problematic | XNB         |
| Font  | XNB  | YES       | JSON And PNG | Font  | JSON And PNG | YES         | XNB         |
| Sound | XNB  | YES       | WAV          | Sound | WAV          | YES         | XNB         |
|       |      |           |              | Music | WMA          | YES         | XNB         |
|       |      |           |              |       |              |             |             |
