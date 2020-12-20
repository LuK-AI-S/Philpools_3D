using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HideUnhideUserbyName : MonoBehaviour
{
    string newLine;
    string[] logEntry;
    public GameObject[] PC;
    FileInfo log = new FileInfo(@"\\philnet-dc1\Userlogs$\sessions.txt");

    public TextMeshProUGUI freeSeatsText;
    public int openSeats;
    

    private void Start()
    {
        openSeats = 30;
        freeSeatsText.text = "Freie Plätze: " + openSeats;
    }

    // Update is called once per frame
    void Update()
    {

        //InitializeComponent();
        //logWatcherTimer.Start();
        logWatcher()


    }

    public void UserJoin(string PCName)
    {
        foreach (var s in PC)
        {
            if (s.name == PCName)
            {
                //Debug.Log(s.name);
                s.SetActive(true);
                openSeats--;
                freeSeatsText.text = "Freie Plätze: " + openSeats;
            }
        }
    }

    public void UserLeave(string PCName)
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

    /*private void logWatcherTimer_Tick(object sender, EventArgs e)
    {
        FileInfo log = new FileInfo(@"\\philnet-dc1\Userlogs$\sessions.txt");
        //if (!logWorker.IsBusy) logWorker.RunWorkerAsync(log);
    }*/ //object sender, DoWorkEventArgs e

    public void logWatcher()
    {
        // Just skip if log file hasn't changed
        if (lastLogLength == log.Length) return;

        // retval

        newLine = string.Empty;
        foreach (string s in logEntry)
        {
            s = string.Empty;
        }

        //FileInfo log = (FileInfo)e.Argument;

        using (StreamReader stream = new StreamReader(log.FullName))
        {
            // Set the position to the last log size and read
            // all the content added
            stream.BaseStream.Position = lastLogLength;
            newLine = stream.ReadToEnd();
        }

        // Keep track of the previuos log length
        lastLogLength = log.Length;

        // Assign the result back to the worker, to be
        // consumed by the form
        //e.Result = newLine;

        string separatingString = " - ";
        logEntry = newLine.Split(separatingString, System.StringSplitOptions.RemoveEmptyEntries);

        if(logEntry[4] == "Anmeldung ")
        {
            UserJoin(logEntry[3]);
        }
        else if(logEntry[4] == "Abmeldung ")
        {
            UserLeave(logEntry[3]);
        }
    }
    


   

}