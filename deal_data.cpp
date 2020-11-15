#include <iostream>
#include <fstream>
#include <string>
#include <cstring>
using namespace std;
int main()
{
	ofstream data("¹ýÂËÊý¾Ý.txt");
	ifstream gdb_out("gdb_output.txt");
	string temp;
	bool flag = false;
	int i = 0;
	while (getline(gdb_out, temp))
	{
		int low = temp.length()-3;
		if (low < 0)
			continue;
		string low_str = temp.substr(low ,3);
		if (low_str == "150")
		{
			if (flag)
			{
				data << "ERROR\n";
			}
			data << "new file\n";
			getline(gdb_out, temp);
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "current->pid=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "current->state=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0; 
			data << "filename=" + temp + "\n";
			getline(gdb_out, temp); 
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "fd=" + temp + "\n";
			flag = true;
		}
		else if (low_str == "158")
		{
			flag = false;
			getline(gdb_out, temp);
			getline(gdb_out, temp);
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "current->close_on_exec=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "filename=" + temp + "\n";
			string file_table = ""; 
			while (getline(gdb_out, temp))
			{ 
				
				file_table += temp;
				int len = temp.length();
				//cout << temp << endl;
				if (temp[len - 1] == '}')
				{
					break;	
				}
			}
			while(file_table[i] != ' ')
			{
				i++;
			}
			file_table = file_table.substr(i+3);
			i = 0;
			//cout << file_table << endl;
			int len = file_table.length();
			int j = 0;
			string cnt = "";
			while (j < len)
			{
				if (file_table[j] == ',')
				{
					data << cnt + "\n";
					cnt = "";
				}
				else 
				{
					cnt += file_table[j];
				}
				j++;
			}
			data << cnt + "\n";
		}
		else if (low_str == "203")
		{
			flag = false;
			getline(gdb_out, temp);
			getline(gdb_out, temp);
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "filp=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "f_mode=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "f_flags=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "f_count=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "f_inode=" + temp + "\n";	
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "f_pos=" + temp + "\n";	
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_mode=" + temp + "\n";	
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_uid=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_size=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_mtime=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_gid=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_nlinks=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_zone[0]=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_zone[1]=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_zone[2]=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_zone[3]=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_zone[4]=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_zone[5]=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_zone[6]=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_zone[7]=" + temp + "\n";
			getline(gdb_out, temp);
			while(temp[i] != ' ')
			{
				i++;
			}
			temp = temp.substr(i+3);
			i = 0;
			data << "i_zone[8]=" + temp + "\n";
		}
	}
}
