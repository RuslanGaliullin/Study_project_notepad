0) My own interpretation of improved text editor.
1) Window settings are independent of each other
2) The timer does not save new files (Unnamed), only those that already exist on the computer, 0 seconds - turn off autosave.
3) If something is called to open in the current tab, but "Cancel" was pressed when selecting a new file, the current tab will still be deleted.
4) If there are no tabs, then "in the current" == "in the new"
5) Saving settings:
     a) The window settings are taken from the last closed form.
     b) New files that the user did not want to save will not be reopened.
     c) When opening, the settings of each text field correspond to the settings of the last saved file (the rightmost tab)
6) Logging will be created in the Logs folder, which lies with the sln, so that there is no problem if there is already such a folder on the computer.
7) Logging is not applicable to new files, only if there is a created file on the computer, you can enable logging, 
    which will save versions starting from the current one.
8) Logging, as well as other settings are saved for all files when reopening, if the last closed file had it enabled
9) The files are not gigantic! no need to overwrite the stack of plz
10) "Text color in the input field" is both to change the color of the selected color, and to switch the input color
11) "Font Customization" is both to change the input font and to change the input font
12) !!! Saving the font and text color in the input field works as follows:
        a) if the last closed file of the last closed form was confused, then the set font and text color are applied to all files
            when opening (they will have the same font and input color). For the whole text!
        b) if the last closed file of the last closed form was non-empty, then the font and color are saved in accordance with the first element
            the last closed file and is applied to all newly opened ones. For the whole text!
