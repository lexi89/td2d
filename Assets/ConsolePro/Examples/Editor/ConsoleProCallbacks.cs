using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using FlyingWormConsole3;

public static class ConsoleProCallbacks
{
	[ConsoleProLogContextMenuAttribute]
	public static void LogContextMenuCallback(GenericMenu inMenu, List<ConsoleProEntry> inEntries)
	{
		inMenu.AddItem(new GUIContent("Test"), false, () =>
		{
			foreach(ConsoleProEntry cEntry in inEntries)
			{
				Debug.Log("Context click: " + cEntry.logText);
			}
		}
		);
	}

	[ConsoleProStackContextMenuAttribute]
	public static void StackContextMenuCallback(GenericMenu inMenu, ConsoleProEntry inEntry, ConsoleProStackEntry inStackEntry)
	{
		inMenu.AddItem(new GUIContent("Test Stack"), false, () =>
		{
			Debug.Log("Stack click: " + inStackEntry.fileNameWithoutAssets + ":" + inStackEntry.lineNumber);
		}
		);
	}

	[ConsoleProStackContextMenuAttribute]
	public static void StackContextMenuRemoveDebugLineCallback(GenericMenu inMenu, ConsoleProEntry inEntry, ConsoleProStackEntry inStackEntry)
	{
		inMenu.AddItem(new GUIContent("Remove From Source"), false, () =>
		{
			string[] lines = System.IO.File.ReadAllLines(inStackEntry.fileName);
			int lineNum = inStackEntry.lineNumber-1;
			if(lineNum > 0 && lineNum < lines.Length)
			{
				Debug.Log("Removing line: " + lines[lineNum]);

				string[] nLines = new string[lines.Length-1];
				int n = 0;
				for(int i = 0; i < lines.Length; i++)
				{
					if(i != lineNum)
					{
						nLines[n] = lines[i];
						n++;
					}
				}
				System.IO.File.WriteAllLines(inStackEntry.fileName, nLines);
				AssetDatabase.Refresh();
			}
		}
		);
	}
}