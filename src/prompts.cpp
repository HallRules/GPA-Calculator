#include "prompts.h"
#include "funcs.h"
#include <iostream>
#include <vector>
using namespace std;

void addClass_prompt(vector<GPA_Class>& classes) {
    system("cls");
    cout << "Enter the name of the class (case sensitive): ";
    string course_name;
    getline(cin, course_name);
    if (course_name == "") {
        cout << "Cannot put class name as blank. Please try again.\n";
        return;
    }
    for (int i = 0; i < classes.size(); i++) {
        if (classes[i].getCourseName() == course_name) {
            cout << "A class with that name already exists. Please try again.\n";
            return;
        }
    }
    cout << "Enter the grade you got in the class (only A,B,C,D,F): ";
    string grade;
    getline(cin, grade);
    grade = strToLower(grade);
    stripWhitespace(grade);
    // No switch case for strings in C++
    if (grade != "a" && grade != "b" && grade != "c" && grade != "d" && grade != "f") {
        cout << "Invalid grade. Please try again.\n";
        return;
    }
    grade[0] = toupper(grade[0]);
    cout << "Enter the number for the type of class (0 for regular, 1 for honors, 2 for AP, 3 for dual enrollment): ";
    string class_type_str;
    getline(cin, class_type_str);
    stripWhitespace(class_type_str);
    if (class_type_str != "0" && class_type_str != "1" && class_type_str != "2" && class_type_str != "3") {
        cout << "Invalid class type. Please try again.\n";
        return;
    }
    switch (stoi(class_type_str)) {
    case 0:
        addClass(classes, course_name, grade[0], classType::regular);
        break;
    case 1:
        addClass(classes, course_name, grade[0], classType::honors);
        break;
    case 2:
        addClass(classes, course_name, grade[0], classType::ap);
        break;
    case 3:
        addClass(classes, course_name, grade[0], classType::dual);
        break;
    default:
        cout << "Error with stoi(class_type_str)\n";
        return;
    }
    cout << "Class added successfully.\n";
    cout << "Class: ";
    printClass(classes[classes.size() - 1]);
}
void removeClass_prompt(vector<GPA_Class>& classes) {
    system("cls");
    cout << "List of classes:\n";
    listClasses(classes);
    cout << "\n\nEnter the name of the class you want to remove (case sensitive):\n";
    string course_name;
    getline(cin, course_name);
    system("cls");
    for (int i = 0; i < classes.size(); i++) {
        if (classes[i].getCourseName() == course_name) {
            classes.erase(classes.begin() + i);
            cout << "Class \"" << course_name << "\" removed successfully.\n\n";
            return;
        }
    }
    cout << "Class \"" << course_name << "\" not found. Please try again.\n\n";
}
void editClass_prompt(vector<GPA_Class>& classes) {

}
void loadClasses_prompt(vector<GPA_Class>& classes) {

}
void calculateGPA_prompt(vector<GPA_Class>& classes) {
    
}