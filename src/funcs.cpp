#include "funcs.h"
#include <string>
#include <fstream>
#include <vector>
#include <iostream>
#include <algorithm>
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
string classTypeToString(classType class_type) {
    string result = "";
    switch (class_type) {
    case classType::regular:
        result = "Regular";
        break;
    case classType::honors:
        result = "Honors";
        break;
    case classType::ap:
        result = "AP";
        break;
    case classType::dual:
        result = "Dual Enrollment";
        break;
    }
    return result;
}
void stripWhitespace(string& str) {
    str.erase(std::remove_if(str.begin(), str.end(), ::isspace), str.end());
}
void printClass(GPA_Class& gpa_class) {
    cout << gpa_class.getCourseName() << " - " << gpa_class.getGrade() << " - " << classTypeToString(gpa_class.getClassType()) << "\n";
}
void printClassByName(const string& course_name, vector<GPA_Class>& classes) {
    for (int i = 0; i < classes.size(); i++) {
        if (classes[i].getCourseName() == course_name) {
            printClass(classes[i]);
            return;
        }
    }
    cout << "Class \"" << course_name << "\" not found. Please try again.\n\n";
}
void addClass(vector<GPA_Class>& classes, const string& course_name, char grade, classType class_type) {
    GPA_Class newClass(course_name, grade, class_type);
    classes.push_back(newClass);
}
void removeClass(vector<GPA_Class>& classes, const string& course_name) {
    for (int i = 0; i < classes.size(); i++) {
        if (classes[i].getCourseName() == course_name) {
            classes.erase(classes.begin() + i);
            cout << "Class \"" << course_name << "\" removed successfully.\n\n";
            return;
        }
    }
    cout << "Class \"" << course_name << "\" not found. Please try again.\n\n";
}
// Edit class function is not implemented here, it is implemented in editClass_prompt
// in prompts.cpp because it is easier to do it there, also I am running out of time
void listClasses(vector<GPA_Class>& classes) {
    system("cls");
    if (classes.size() == 0) {
        cout << "No classes found.\n\n";
        return;
    }
    cout << "Classes: (Name, Grade, Class Type)\n\n";
    for (int i = 0; i < classes.size(); i++) {
        cout << classes[i].getCourseName() << " - " << classes[i].getGrade() << " - " << classTypeToString(classes[i].getClassType()) << "\n";
    }
    cout << "\n\n";
}
void calculateGPA(const vector<GPA_Class>& classes, bool weighted){

}
void loadClasses(const string& filename, vector<GPA_Class>& classes) {
    ifstream loadfile;
    loadfile.open(filename);
    // put stuffs here
    loadfile.close();
}
void saveClasses(const string& filename, vector<GPA_Class>& classes) {
    ofstream savefile;
    savefile.open(filename);
    // put stuffs here
    savefile.close();
}