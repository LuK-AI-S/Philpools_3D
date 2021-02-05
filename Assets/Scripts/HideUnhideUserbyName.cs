using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HideUnhideUserbyName : MonoBehaviour
{
    public GameObject[] PC;
    string LogName = @"C:\Users\Dennis\Desktop\sessions.txt";
    string logLocation = @"C:\Users\Dennis\Desktop\";

    public TextMeshProUGUI freeSeatsText;
    public int openSeats;

    // Start is called at the Start of the application
    void Start()
    {
        // initiateState();
        
         foreach (var s in PC)
        {
            s.SetActive(false);
            openSeats++;
        }
        CreateFileWatcher(logLocation);
        //freeSeatsText.text = "Freie Plätze: " + openSeats;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // UserJoin/Leave are the two hide/unhide functions to disable visibility of the Users in the 3D modell
    void UserJoin(string PCName)
    {
        Debug.Log(PCName);
        Debug.Log(PC[10].name); // no Result

        foreach (var s in PC)
        {
            Debug.Log(s.name); //
            Debug.Log(PCName); //
            if (s.name == PCName)
            {
                s.SetActive(true);
                Debug.Log(s); //
                Debug.Log("UserJoin " + PCName); //
                openSeats--;
                freeSeatsText.text = "Freie Plätze: " + openSeats;
            }
        }
    }

    void UserLeave(string PCName)
    {
        foreach (var s in PC)
        {
            if (s.name == PCName)
            {
                s.SetActive(false);
                openSeats++;
                freeSeatsText.text = "Freie Plätze: " + openSeats;
            }
        }
    }

    public void CreateFileWatcher(string path)
    {
        // Create a new FileSystemWatcher and set its properties.
        FileSystemWatcher watcher = new FileSystemWatcher();
        watcher.Path = path;
        /* Watch for changes in LastAccess and LastWrite times, and 
        the renaming of files or directories. */
        watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite 
        | NotifyFilters.FileName | NotifyFilters.DirectoryName;
        // Only watch text files.
        watcher.Filter = "*.txt";

        // Add event handlers.
        watcher.Changed += new FileSystemEventHandler(OnChanged);
        watcher.Created += new FileSystemEventHandler(OnChanged);
        watcher.Deleted += new FileSystemEventHandler(OnChanged);

        // Begin watching.
        watcher.EnableRaisingEvents = true;
    }

    // Define the event handlers.
    private void OnChanged(object source, FileSystemEventArgs e)
    {
        // Specify what is done when a file is changed, created, or deleted.
        if (e.FullPath == LogName)
        {
            string[] logEntry;
            string line; 
            string lastLine = string.Empty;

            // Read last line of Sessions.txt and store it into LastLine String
            System.IO.StreamReader file = new System.IO.StreamReader(LogName);
            while((line = file.ReadLine()) != null)
            {  
              lastLine = line;   
            }
            file.Close();

            string[] separatingString = { " - " };
            logEntry = lastLine.Split(separatingString, System.StringSplitOptions.RemoveEmptyEntries);


            // Check the 4th entry can be for "Anmeldung " and "Abmeldung " and then pass the 3rd Log entry to the hiding/unhiding function wich is the PC name.
            if(logEntry[4] == "Anmeldung ")
            {
                Debug.Log("Anmeldung " + logEntry[2]); //
                UserJoin(logEntry[2]);
            }
            else if(logEntry[4] == "Abmeldung ")
            {
                UserLeave(logEntry[2]);
                Debug.Log("Abmeldung " + logEntry[2]); //
            }
        }
    }

    /*void initiateState()
    {
        string[] logEntry;
        string line; 
        string lastLine;
        string pcNumber;

        // Count threw the array of Pcs to get the PC numbers in the current Window
        foreach (var s in PC)
        {
            s.SetActive(false);
            openSeats++;
        }
        freeSeatsText.text = "Freie Plätze: " + openSeats;

        System.IO.StreamReader file = new System.IO.StreamReader(LogName);
        while((line = file.ReadLine()) != null)
        {  
            string[] separatingString = { " - " };
            logEntry = line.Split(separatingString, System.StringSplitOptions.RemoveEmptyEntries);

            if (logEntry[0].Contains(Day(date)));
            {
                string b = string.Empty;
                string a = logEntry[2];
                for (int i=0; i< a.Length; i++)
                {
                    if (Char.IsDigit(a[i]))
                    b += a[i];
                }

                if (b.Length>0);
                {
                    pcNumber = int.Parse(b);
                }

                pcNumber.TrimStart(new Char[] { '0' } );

                if(logEntry[4] == "Anmeldung " && logEntry[2].Contains(pcNumber));
                {
                    UserJoin(logEntry[2]);
                }
            }
        }
        file.Close();
    }*/
}