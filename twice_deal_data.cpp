#include <iostream>
#include <fstream>
#include <string>
#include <cstring>
using namespace std;
int main()
{
	ofstream twice("二次过滤.txt");
	ifstream once("过滤数据.txt");
	string temp;
	string new_temp = "";
	while (getline(once, temp))
	{
		if(temp == "new file")
		{
			twice << "new file\n";
			continue;	
		} 
		int len = temp.length();
		bool flag = false;
		for (int i = 0; i < len; i++)
		{
			if (temp[i] == '\'')
			{
				break;
			}
			else if (temp[i] == ' ' || temp[i] == '}' || temp[i] == '>' || temp[i] == ')')
			{
				continue;
			}
			else if (temp[i] == '"')
			{
				if (flag)
				{
					continue;
				}
				new_temp = new_temp + "###";
				flag = true;
			}
			else if (temp[i] == '{')
			{
				if (temp[i + 1] == '{')
				{
					twice << "file table\n"; 
				}
				else
				{
					twice << "new term\n";
				}
			}
			else if (temp[i] == '<')
			{
				if (temp[i + 1] == 'i')
				{
					new_temp = new_temp + "###";
					i += strlen("inode_table+");
				}
				else if(temp[i + 1] == 'r' && temp[i + 2] == 'e' && temp[i + 3] == 'p')
				{
					new_temp = new_temp + "\nrepeats=";
					i += strlen("repeats ");	
				}
				else if(temp[i + 1] == 'f')
				{
					new_temp = new_temp + "###";
					i += strlen("file_table+");
				}
				else if(temp[i + 1] == 's' && temp[i + 2] == 't')
				{
					new_temp = new_temp + "###0";
					break;
				}
				else 
				{
					break;
				}
			}
			else if (temp[i] == '(')
			{
				while(temp[i] != ')')
				{
					i++;
				}
				i++;
			}
			else
			{
				new_temp = new_temp + temp[i];	 
			} 	
		}
		twice << new_temp + "\n";
		new_temp = "";
	}
	return 0;	
} 
