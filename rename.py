import os

nameToReplace = "BlogSimples"
newName = "GbiTestCadastro"
initialPath = "./"
foldersToIgnore = ["bin", "obj", "debug", ".git", ".vs", "bkp"]
filesToIgnore = [".gitignore", ".gitmodules", ".dockerignore", "rename.py"]


def renameFileData(filename):
    fin = open(filename, 'rt', encoding="utf-8")
    data = fin.read()
    data = data.replace(nameToReplace, newName)
    fin.close()

    fin = open(filename, 'wt', encoding="utf-8")
    fin.write(data)
    fin.close()

def renameFileName(path, filename):
    renamed = filename.replace(nameToReplace, newName)
    src = path + filename
    dst = path + renamed
    os.rename(src, dst)

def getInFolder(path):
    for count, filename in enumerate(os.listdir(path)):
        fileWithPath = path + filename
        isFolder = os.path.isdir(fileWithPath)
        if isFolder:
            pathFolder = fileWithPath + '/'
            if not any(filename in s for s in foldersToIgnore):
                getInFolder(pathFolder)
        else:
            if not any(filename in s for s in filesToIgnore):
                renameFileData(fileWithPath)

        if filename.find(nameToReplace) != -1:
            renameFileName(path, filename)

def main():
    getInFolder(initialPath)

if __name__ == "__main__":
    main()
