
[=========================================================================]


Rootsploit - tool for bypass UAC. Original tool -
https://github.com/exploitblizzard/UAC-Bypass


This tool is a modernized and improved project of exploitblizzard.

==========================================================================


Rootsploit - инструмент для обхода контроля учётных записей (UAC). Оригинальный проект - 
https://github.com/exploitblizzard/UAC-Bypass

Данный инструмент - это улучшенный и доработанный проект exploitblizzard

[=========================================================================]

This tool helps to run apps or py scripts bypassing UAC

how works this exploit:

[1] - make registry key - HKEY_CURRENT_USER\Software\Classes\ms-settings\shell\open\command

[2] - set fullpath to file as parameter for empty string in HKEY_CURRENT_USER\Software\Classes\ms-settings\shell\open\command

[3] - make DWORD parameter DelegateExecute with value 0

[4] - run computerdefaults.exe (WIN + R computerdefaults.exe)



{=========================}
Создано только в учебных и
исследовательских целях
{=========================}
