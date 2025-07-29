using System.Text;

namespace FileSystemCommands
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Project Idea : File System Commands [ List - Info - Print - Remove - Move - Copy - Write - Make Directory - Exit ] ");
            Console.WriteLine("1- list + path [List all the Files and Directories] ");
            Console.WriteLine("2- info + path [Informations about the Files or Directories]");
            Console.WriteLine("3- print + path [Print the File Content]");
            Console.WriteLine("4- write + path [Write in the File]");
            Console.WriteLine("5- move + path [Move File to another Folder]");
            Console.WriteLine("6- copy + path [Copy File to another Folder]");
            Console.WriteLine("7- remove + path [Remove Files or Directories]");
            Console.WriteLine("8- mkdir + path [Make Directory in the Folder]");
            Console.WriteLine("9- exit [Program Stop]");

            while (true)
            {
                Console.Write(">> ");
                var Input = Console.ReadLine().Trim();

                if (Input == "exit")
                {
                    Console.WriteLine("The Process is Done Successfully ^_^");
                    break;
                }

                var WhiteSpacesIndex = Input.IndexOf(' ');
                var command = Input.Substring(0, WhiteSpacesIndex).ToLower();
                var path = Input.Substring(WhiteSpacesIndex + 1).Trim();

                if (command == "list")
                {
                    foreach (var entry in Directory.GetDirectories(path))
                        Console.WriteLine($"\t[Dir] {entry}");
                    foreach (var entry in Directory.GetFiles(path))
                        Console.WriteLine($"\t[File] {entry}");
                    //foreach (var entry in Directory.GetFileSystemEntries(path))
                    //    Console.WriteLine($"\t[File] {entry}");
                    Console.WriteLine("\n\nThe Process is Done Successfully ^_^");
                }
                else if (command == "info")
                {
                    if (Directory.Exists(path))
                    {
                        var dirInfo = new DirectoryInfo(path);

                        Console.WriteLine("Types : Directory");
                        Console.WriteLine($"Name : {dirInfo.Name}");
                        Console.WriteLine($"Created At : {dirInfo.CreationTime}");
                        Console.WriteLine($"Last Modified At : {dirInfo.LastWriteTime}");
                        Console.WriteLine($"Last Access At : {dirInfo.LastAccessTime}");
                        Console.WriteLine("\n\nThe Process is Done Successfully ^_^");
                    }
                    else if (File.Exists(path))
                    {
                        var fileInfo = new FileInfo(path);

                        Console.WriteLine("Types : File");
                        Console.WriteLine($"Name : {fileInfo.Name}");
                        Console.WriteLine($"Created At : {fileInfo.CreationTime}");
                        Console.WriteLine($"Last Modified At : {fileInfo.LastWriteTime}");
                        Console.WriteLine($"Last Access At : {fileInfo.LastAccessTime}");
                        Console.WriteLine($"Sizes in Bytes : {fileInfo.Length}");

                        Console.WriteLine("\n\nThe Process is Done Successfully ^_^");
                    }
                    else
                        Console.WriteLine("Can not Find Files or Directories in this Path !! Check the Folder Content First !! The Process is not Done .");
                }
                else if (command == "print")
                {
                    if (File.Exists(path))
                    {
                        var content = File.ReadAllText(path);
                        Console.WriteLine(content);
                        Console.WriteLine("\n\nThe Process is Done Successfully ^_^");
                    }
                    else
                        Console.WriteLine("Can not Find Files in this Path !! Check the Files First !! The Process is not Done .");

                }
                else if (command == "write")
                {
                    if (File.Exists(path))
                    {
                        Console.WriteLine("Write the Content here : ");
                        var content = Console.ReadLine(); // + Environment.NewLine;
                    Found:
                        Console.Write("Should I write in the same file ? [y/n]");
                        var Reply = Console.ReadLine();
                        if (Reply == "y")
                            File.WriteAllText(path, content);
                        else if (Reply == "n")
                        {
                        Found2:
                            Console.Write("Delete the existing one and write in its place ? [y/n]");
                            var Replay2 = Console.ReadLine();
                            if (Replay2 == "y")
                                File.AppendAllText(path, content);
                            else if (Replay2 == "n")
                                goto Found;
                            else
                            {
                                Console.WriteLine("Invalid Replay !!");
                                goto Found2;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Replay !!");
                            goto Found;
                        }
                        Console.WriteLine("The Process is Done Successfully ^_^");
                    }
                    else
                        Console.WriteLine("Can not Find Files in this Path !! Check the Files First ?? The Process is not Done .");

                }
                else if (command == "remove")
                {
                    string[] Dires = Directory.GetDirectories(path);
                    string[] files = Directory.GetFiles(path);

                Found:
                    Console.Write("choose Option to Delete [1] Folder \t\t  [2] File ? ");
                    var Replay = int.Parse(Console.ReadLine().ToLower());

                    if (Replay == 1)
                    {
                        Console.WriteLine("Please Enter The Folder Name: ");
                        var FolderName = Console.ReadLine();

                        foreach (string dir in Dires)
                        {
                            if (FolderName == Path.GetFileNameWithoutExtension(dir))
                            {
                            Found2:
                                Console.Write("You Want to Delete all the content in Directory : [y/n]");
                                var Reply = Console.ReadLine();
                                if (Reply == "y")
                                    Directory.Delete(dir, true);
                                else if (Reply == "n")
                                    Directory.Delete(dir);
                                else
                                {
                                    Console.WriteLine("Invalid Reply !!");
                                    goto Found2;
                                }
                                break;
                            }
                        }
                        Console.WriteLine("The Process is Done Successfully ^_^");
                    }
                    else if (Replay == 2)
                    {
                        Console.WriteLine("Please Enter The File Name: ");
                        var FileName = Console.ReadLine();

                        foreach (string file in files)
                        {
                            if (FileName == Path.GetFileNameWithoutExtension(file))
                            {
                                File.Delete(file);
                                break;
                            }
                        }
                        Console.WriteLine("The Process is Done Successfully ^_^");
                    }
                    else
                    {
                        Console.WriteLine("The Option is Invalid !!.");
                        goto Found;
                    }
                }
                else if (command == "mkdir")
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine("The Process is Done Successfully ^_^");
                }
                else if (command == "move")
                {
                    string[] files = Directory.GetFiles(path);
                    Console.Write("Enter the Folder Path: ");
                    var DestinationFolder = Console.ReadLine();
                    Console.Write("Enter the File Name: ");
                    var FileName = Console.ReadLine();

                    foreach (string file in files)
                    {
                        if (FileName == Path.GetFileNameWithoutExtension(file))
                        {
                            File.Move(file, $"{DestinationFolder}{Path.GetFileName(file)}");
                            break;
                        }
                    }
                    Console.WriteLine("The Process is Done Successfully ^_^");
                }
                else if (command == "copy")
                {
                    string[] files = Directory.GetFiles(path);
                    Console.Write("Enter the Folder Path: ");
                    var DestinationFolder = Console.ReadLine();
                    Console.Write("Enter the File Name: ");
                    var FileName = Console.ReadLine();

                    foreach (string file in files)
                    {
                        if (FileName == Path.GetFileNameWithoutExtension(file))
                        {
                            File.Copy(file, $"{DestinationFolder}{Path.GetFileName(file)}");
                            break;
                        }
                    }
                    Console.WriteLine("The Process is Done Successfully ^_^");
                }
                
            }
        }
    }
}
