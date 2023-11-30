#include <iostream>
#include <vector>
#include "funcs.h"
#include "classes.h"
#include "prompts.h"
using namespace std;

// GPA Calculator by Michael Hall for FBLA 2023

/*
TODO:
- Add the ability to edit classes (by name)
- Add the ability to calculate the total GPA
- Add the ability to save and load classes from a savefile
*/

// This is the array that stores all the classes that the user wants to calculate for
vector<GPA_Class> classes;

// The main loop that handles the commands
int main_loop() {
    // This prints all the tutorial commands to the console so the user knows what the commands are
    cout << "Enter a command to continue: add, remove, edit, list, load, calculate, quit\n\n";
    cout << "add: Adds a new class to the list of classes you want to calculate for\n";
    cout << "remove: Removes a class from the list of classes\n";
    cout << "edit: Edits a class in the list of classes\n";
    cout << "load: Loads a savefile with the given name (has to be in the same directory)\n";
    cout << "list: Lists all the classes in the list of classes\n";
    cout << "calculate: Calculates the GPA of the classes in the list of classes, both weighted and unweighted\n";
    cout << "quit: Quits the program with the option to save\n\n\n";

    string command;
    getline(cin, command);
    command = strToLower(command);
    stripWhitespace(command);
    
    // Since C++ doesn't support switch/case for strings, I had to spam if/else statements
    if (command == "add") {
        addClass_prompt(classes);
    }
    else if (command == "remove") {
        removeClass_prompt(classes);
    }
    else if (command == "edit") {
        editClass_prompt(classes);
    }
    else if (command == "list") {
        listClasses(classes);
    }
    else if (command == "load") {
        loadClasses_prompt(classes);
    }
    else if (command == "calculate") {
        calculateGPA(classes);
    }
    else if (command == "quit") {
        // This breaks the loop to start the saving and quitting process
        return 0;
    }
    else {
        system("cls");
        cout << "Invalid command. Please try again.\n";
    }

    // To continue the loop and not quit the program, we return 1
    return 1;
}

int main() {
    system("cls");
    // Breaks when return is less than 1, which is when the user wants to quit
    while (main_loop());

    // This last code handles the saving and quitting, because when the main loop returns 0 it will break the loop.
    string save;
    do {
        cout << "Would you like to save your classes? (y/n): ";
        getline(cin, save);
        stripWhitespace(save);
        if (tolower(save[0]) == 'n') {
            return 0;
        }
    } while (tolower(save[0]) != 'y');
    cout << "Enter the name for the savefile: ";
    string savefile_name;
    getline(cin, savefile_name);
    saveClasses(savefile_name, classes);
    return 0;
}