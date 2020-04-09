using Module2Task;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace XUnitTestFileVisitor
{
    //??? ???? ?????? ?? ??????
    // ??????? ?? ?????? ????? ?? ?????? ????? ??? ????? ??????  ?? ????????? ?????? ? ??????? ??????(?????????? ?????? , ?????)
    
    public class UnitTFileSystemVisitor
    {
        [Fact]
        public void GetAllElenments()
        {
            // Arrange
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\Folder\New File.txt", new MockFileData("some new file") },
                { @"c:\Folder 2\setting.json", new MockFileData("\"field\":\"value\"")}
            });

            FileSystemVisitor fsv = new FileSystemVisitor("c:/", fileSystem : fileSystem);
            var expected = new string[]{ 
                @"c:\Folder", 
                @"c:\Folder\New File.txt", 
                @"c:\Folder 2", 
                @"c:\Folder 2\setting.json", 
                @"c:\myfile.txt" };

            // Act
            var actual =  fsv.GetAllElements(); 

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFilteredElementByName()
        {
            // Arrange
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("") },
                { @"c:\Folder\New File.txt", new MockFileData("") },
                { @"c:\New Folder\setting.json", new MockFileData("")}
            });

            FileSystemVisitor fsv = new FileSystemVisitor("c:/", x=>x.Contains("New"), 
                fileSystem: fileSystem);
            var expected = new string[]{ 
                @"c:\Folder\New File.txt",
                @"c:\New Folder"};

            // Act
            var actual = fsv.GetAllElements();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFilteredElementByNameExcludeFile()
        {
            // Arrange
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("") },
                { @"c:\Folder\New File.txt", new MockFileData("") },
                { @"c:\New Folder\setting.json", new MockFileData("")}
            });

            FileSystemVisitor fsv = new FileSystemVisitor("c:/", x => x.Contains("New"),
                fileSystem: fileSystem, excludeFile: true);
            var expected = new string[]
            {
                @"c:\New Folder"
            };

            // Act
            var actual = fsv.GetAllElements();

            // Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetFilteredElementByNameExcludeFolder()
        {
            // Arrange
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("") },
                { @"c:\Folder\New File.txt", new MockFileData("") },
                { @"c:\New Folder\setting.json", new MockFileData("")}
            });

            FileSystemVisitor fsv = new FileSystemVisitor("c:/", x => x.Contains("New"),
                fileSystem: fileSystem, excludeFolder: true);
            var expected = new string[]{
                @"c:\Folder\New File.txt"};

            // Act
            var actual = fsv.GetAllElements();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFilteredElementByNeedStop()
        {
            // Arrange
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("") },
                { @"c:\Folder\New File.txt", new MockFileData("") },
                { @"c:\New Folder\setting.json", new MockFileData("")}
            });

            FileSystemVisitor fsv = new FileSystemVisitor("c:/", x => x.Contains("New"),
                fileSystem: fileSystem, needStop: true);
            var expected = new string[]
            {
                @"c:\Folder\New File.txt"
            };

            // Act
            var actual = fsv.GetAllElements();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
