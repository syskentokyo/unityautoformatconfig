https://github.com/syskentokyo/unityautoformatconfig/wiki


# About

This asset allows you to automatically configure settings for each platform when importing sounds, textures, and videos.

# Function

* Automatically configure compression and other settings for each platform when importing sounds, textures, and videos
* Can be formatted for specified directories
* Automatically configures Standalone, iOS, Android, and TVOS platforms.


# About common configuration files

File location


Assets/SyskenTLib/UnityAutoFormatConfig/Editor/Config/GeneralConfig.asset

## What you can set with this file


* Adjust the timing of automatic settings
  * Target only on first import, also on re-import, do nothing


* Change the path of the specified directory for each format




# About configuration files for each file type

File location

* Assets/SyskenTLib/UnityAutoFormatConfig/Editor/Config/Audio
* Assets/SyskenTLib/UnityAutoFormatConfig/Editor/Config/Texture
* Assets/SyskenTLib/UnityAutoFormatConfig/Editor/Config/Video

## What you can set with this file

* Change automatic setting settings


## supplement

For textures, maximum resolution, compression format, etc.
For sound, sample rate etc.
For video, compression format etc.
It is possible to change the setting items as shown above.
