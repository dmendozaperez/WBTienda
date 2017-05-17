using System;
using System.Collections.Generic;
using System.Web;
using DevExpress.Web;
public class DateTimeSortedFileSystemProvider : DevExpress.Web.PhysicalFileSystemProvider
{
    public DateTimeSortedFileSystemProvider(string rootFolder)
        : base(rootFolder) { }

    public override IEnumerable<DevExpress.Web.FileManagerFile> GetFiles(DevExpress.Web.FileManagerFolder folder)
    {
        List<DevExpress.Web.FileManagerFile> files = new List<DevExpress.Web.FileManagerFile>(base.GetFiles(folder));
        files.Sort(delegate(DevExpress.Web.FileManagerFile file1, DevExpress.Web.FileManagerFile file2)
        {
            return GetLastWriteTime(file2).CompareTo(GetLastWriteTime(file1));
        });
        return files;
    }
}