#include "prompts.h"
#include "funcs.h"
#include <iostream>
#include <vector>
using namespace std;

void addClass_prompt(vector<GPA_Class>& classes) {
    clear_screen();
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
    clear_screen();
    cout << "List of classes:\n";
    listClasses(classes);
    cout << "\n\nEnter the name of the class you want to remove (case sensitive):\n";
    string course_name;
    getline(cin, course_name);
    clear_screen();
    removeClass(classes, course_name);
}
void editClass_prompt(vector<GPA_Class>& classes) {
    clear_screen();
    cout << "List of classes:\n";
    listClasses(classes);
    cout << "\n\nEnter the name of the class you want to edit (case sensitive):\n";
    string course_name;
    getline(cin, course_name);
    bool found = false;
    for (int i = 0; i < classes.size(); i++) {
        if (classes[i].getCourseName() == course_name) {
            found = true;
            break;
        }
    }
    if (!found) {
        cout << "Class \"" << course_name << "\" not found. Please try again.\n\n";
        return;
    }
    clear_screen();
    printClassByName(course_name, classes);
    cout << "Type the number of the attribute you want to edit (0 for class name, 1 for grade, 2 for type of class):\n";
    string attribute_str;
    getline(cin, attribute_str);
    stripWhitespace(attribute_str);
    if (attribute_str != "0" && attribute_str != "1" && attribute_str != "2") {
        cout << "Invalid attribute. Please try again.\n\n";
        return;
    }
    int attribute = stoi(attribute_str);
    // I'm sorry for this nesting I'm just too tired bruh
    if (attribute == 0) {
        cout << "Enter the new name of the class:\n";
        string new_course_name;
        getline(cin, new_course_name);
        for (int i = 0; i < classes.size(); i++) {
            if (classes[i].getCourseName() == new_course_name) {
                cout << "A class with that name already exists. Please try again.\n";
                return;
            }
        }
        for (int i = 0; i < classes.size(); i++) {
            if (classes[i].getCourseName() == course_name) {
                classes[i].setCourseName(new_course_name);
                cout << "Class \"" << course_name << "\" name successfully changed to \"" << new_course_name << "\".\n\n";
                return;
            }
        }
    }
    else if (attribute == 1) {
        cout << "Enter the new grade of the class (A,B,C,D,F):\n";
        string new_grade;
        getline(cin, new_grade);
        new_grade = strToLower(new_grade);
        stripWhitespace(new_grade);
        if (new_grade != "a" && new_grade != "b" && new_grade != "c" && new_grade != "d" && new_grade != "f") {
            cout << "Invalid grade. Please try again.\n";
            return;
        }
        new_grade[0] = toupper(new_grade[0]);
        for (int i = 0; i < classes.size(); i++) {
            if (classes[i].getCourseName() == course_name) {
                classes[i].setGrade(new_grade[0]);
                cout << "Class \"" << course_name << "\" grade successfully changed to \"" << new_grade << "\".\n\n";
                return;
            }
        }
    }
    else if (attribute == 2) {
        cout << "Enter the new type of the class (0 for regular, 1 for honors, 2 for AP, 3 for dual enrollment):\n";
        string new_class_type_str;
        getline(cin, new_class_type_str);
        stripWhitespace(new_class_type_str);
        if (new_class_type_str != "0" && new_class_type_str != "1" && new_class_type_str != "2" && new_class_type_str != "3") {
            cout << "Invalid class type. Please try again.\n";
            return;
        }
        classType new_class_type;
        switch (stoi(new_class_type_str)) {
        case 0:
            new_class_type = classType::regular;
            break;
        case 1:
            new_class_type = classType::honors;
            break;
        case 2:
            new_class_type = classType::ap;
            break;
        case 3:
            new_class_type = classType::dual;
            break;
        default:
            cout << "Error with stoi(new_class_type_str)\n";
            return;
        }
        for (int i = 0; i < classes.size(); i++) {
            if (classes[i].getCourseName() == course_name) {
                classes[i].setClassType(new_class_type);
                cout << "Class \"" << course_name << "\" type successfully changed to \"" << classTypeToString(new_class_type) << "\".\n\n";
                return;
            }
        }
    }
}
void loadClasses_prompt(vector<GPA_Class>& classes) {
    clear_screen();
    cout << "Enter the name of the savefile (has to be in the same directory):\n";
    string filename;
    getline(cin, filename);
    if (loadClasses(filename, classes)) {
        cout << "Savefile \"" << filename << "\" loaded successfully.\n\n";
    }
    else {
        cout << "Savefile \"" << filename << "\" not found or another error occured while loading.\nTry typing the command \"quit\" and saving to create a savefile, then try again.\n\n";
    }
}