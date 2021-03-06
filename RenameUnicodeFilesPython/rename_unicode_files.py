# -*- coding: utf-8 -*-
import os
import time
from tkinter import Tk, filedialog

root = Tk()  # pointing root to Tk() to use it as Tk() in program.
root.withdraw()  # Hides small tkinter window.
root.attributes('-topmost', True)  # Opened windows will be active. above all windows despite of selection.
open_folder = filedialog.askdirectory()

# tree = os.walk('C:\\test\\Unicode_files')
tree = os.walk(open_folder)

for address, dirs, files in tree:
	for file in files:
		print("")
		print("File check: {}".format(file))
		file_path = address + "\\" + file
		clean_file = ""
		unicode_test = False
		for symbl in file:
			if symbl.isprintable():
				clean_file += symbl
			else:
				unicode_test = True
		if unicode_test:
			print("Unicode found")
			# print(clean_file)
			new_file_path = address + "\\" + clean_file
			if os.path.exists(new_file_path):
				while os.path.exists(new_file_path):
					new_file_path = new_file_path.replace(".", "_.")
					time.sleep(1)
					# print(new_file_path)
			print("Clean new file name: {}".format(new_file_path))
			os.rename(file_path, new_file_path)
