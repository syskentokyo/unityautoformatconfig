using System.Collections.Generic;
using UnityEngine;

namespace SyskenTLib.UnityAutoFormatConfig
{
    public enum FormatTiming:int    
    {
        None=0
        ,FirstOnly=10
        ,EveryTime=100
    }
    
    
    public class STAutoFormatConfig : ScriptableObject
    {
        [Header("設定")]
        public FormatTiming compileTimingTexture = FormatTiming.FirstOnly;
        public FormatTiming compileTimingAudio = FormatTiming.FirstOnly;
        public FormatTiming compileTimingVideo = FormatTiming.FirstOnly;

        [Header("ディレクトリ")] 
        [Header("テクスチャ")]
        public List<string> normalUITextureDirectoryPathList = new List<string>()
        {
            "Asset/Main/Texture/UI"
            ,"Asset/Client/Texture/UI"
        };
        
        public List<string> dotUITextureDirectoryPathList = new List<string>()
        {
            "Asset/Main/Texture/DotUI"
            ,"Asset/Client/Texture/DotUI"
        };
        
        public List<string> custom1TextureDirectoryPathList = new List<string>()
        {
            "Asset/Main/Texture/Custom1"
            ,"Asset/Client/Texture/Custom1"
        };
        
        public List<string> custom2TextureDirectoryPathList = new List<string>()
        {
            "Asset/Main/Texture/Custom2"
            ,"Asset/Client/Texture/Custom2"
        };
        
        public List<string> custom3TextureDirectoryPathList = new List<string>()
        {
            "Asset/Main/Texture/Custom3"
            ,"Asset/Client/Texture/Custom3"
        };
        
        
        [Header("Audio")]
        public List<string> bgmAudioDirectoryPathList = new List<string>()
        {
            "Asset/Main/Audio/BGM"
            ,"Asset/Client/Audio/BGM"
        };
        
        public List<string> seAudioDirectoryPathList = new List<string>()
        {
            "Asset/Main/Audio/SE"
            ,"Asset/Client/Audio/SE"
        };
        
        public List<string> custom1AudioDirectoryPathList = new List<string>()
        {
            "Asset/Main/Audio/Custom1"
            ,"Asset/Client/Audio/Custom1"
        };
        
        public List<string> custom2AudioDirectoryPathList = new List<string>()
        {
            "Asset/Main/Audio/Custom2"
            ,"Asset/Client/Audio/Custom2"
        };
        
        public List<string> custom3AudioDirectoryPathList = new List<string>()
        {
            "Asset/Main/Audio/Custom3"
            ,"Asset/Client/Audio/Custom3"
        };
        
        [Header("Video")]
        public List<string> normalVideoDirectoryPathList = new List<string>()
        {
            "Asset/Main/Video/Normal"
            ,"Asset/Client/Video/Normal"
        };
        
        public List<string> custom1VideoDirectoryPathList = new List<string>()
        {
            "Asset/Main/Video/Custom1"
            ,"Asset/Client/Video/Custom1"
        };
        
        public List<string> custom2VideoDirectoryPathList = new List<string>()
        {
            "Asset/Main/Video/Custom2"
            ,"Asset/Client/Video/Custom2"
        };
        
        public List<string> custom3VideoDirectoryPathList = new List<string>()
        {
            "Asset/Main/Video/Custom3"
            ,"Asset/Client/Video/Custom3"
        };
    }
}