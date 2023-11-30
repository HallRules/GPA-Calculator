#include "funcs.h"
#include <string>
#include <fstream>
#include <vector>
#include <iostream>
#include "classes.h"
using namespace std;

// The actual code for the functions defined in funcs.h
string strToLower(const string& str) {
    string newStr = "";
    for (int i = 0; i < str.length(); i++) {
        newStr += tolower(str[i]);
    }
    return newStr;
}
void listClasses(const vector<GPA_Class*>& classes) {
    system("cls");
    for (int i = 0; i < classes.size(); i++) {
        cout << classes[i]->getCourseName() << endl;
    }
}
void saveClasses(const string& filename, const vector<GPA_Class*>& classes) {
    ofstream savefile;
    savefile.open(filename);
    // put stuffs here
    savefile.close();
}