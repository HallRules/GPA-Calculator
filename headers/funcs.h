#pragma once
#include "classes.h"
#include <string>
#include <vector>
using namespace std;

// Definitions for the functions used in main.cpp
string strToLower(const string& str);
string classTypeToString(classType class_type);
void stripWhitespace(string& str);
void printClass(GPA_Class& gpa_class);
void printClassByName(const string& course_name, vector<GPA_Class>& classes);
void addClass(vector<GPA_Class>& classes, const string& course_name, char grade, classType class_type);
void removeClass(vector<GPA_Class>& classes, const string& course_name);
void listClasses(vector<GPA_Class>& classes);
void calculateGPA(vector<GPA_Class>& classes);
int loadClasses(const string& filename, vector<GPA_Class>& classes);
void saveClasses(const string& filename, vector<GPA_Class>& classes);