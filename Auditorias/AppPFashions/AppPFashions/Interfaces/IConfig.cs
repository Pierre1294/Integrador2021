using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AppPFashions.Interfaces
{
    public interface IConfig
    {
        string DirectoryDB { get; }

        ISQLitePlatform Platform { get; }
        
    }

    public interface IFileManager
    {
        void OpenFile(string filePath);
        void DeleteFile(string filePath);

    }

    public interface ISpeechToText
    {
        Task<string> SpeechToTextAsync();
    }

    public interface IDownloader
    {
        void DownloadFile(string url, string folder);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
        void InstallAPK();
        
        void Show(string message);        
        void Hide();
    }

    public class DownloadEventArgs : EventArgs
    {
        public bool FileSaved = false;
        public DownloadEventArgs(bool fileSaved)
        {
            FileSaved = fileSaved;
        }
    }
}
