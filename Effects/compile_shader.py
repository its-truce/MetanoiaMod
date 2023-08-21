import os
import time
import shutil

def main():
    filepath = input("Enter the filepath value: ")
    destination_folder = r"F:\fxcompiler"
    final_destination = fr"C:\Users\raush\OneDrive\Documents\My Games\Terraria\tModLoader\ModSources\Metanoia\Content\Effects"

    destination_fx = os.path.join(destination_folder, f"{filepath}.fx")
    os.makedirs(destination_folder, exist_ok=True)
    shutil.copy2(fr"C:\Users\raush\OneDrive\Documents\My Games\Terraria\tModLoader\ModSources\Metanoia\Content\Effects\{filepath}.fx", destination_fx)

    os.chdir(destination_folder)
    os.system("fxcompiler.exe")

    time.sleep(4)

    destination_xnb = os.path.join(final_destination, f"{filepath}.xnb")
    shutil.copy2(os.path.join(destination_folder, f"{filepath}.xnb"), destination_xnb)

    print("Process completed successfully.")

if __name__ == "__main__":
    main()
