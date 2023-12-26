import React, { Component } from 'react';

export class Calculator extends Component {
    static displayName = Calculator.name;

    constructor(props) {
        super(props);
        this.state = {
            classes: [{ className: '', classType: 'Regular', grade: 'A+' }],
            data: null,
            unweightedGPA: null,
            weightedGPA: null,
            studentID: '0712842'
        };
        this.addClass = this.addClass.bind(this);
        this.removeClass = this.removeClass.bind(this);
        this.calculateGPA = this.calculateGPA.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.fetchData = this.fetchData.bind(this);
        this.handleStudentIDChange = this.handleStudentIDChange.bind(this);
        this.handleSaveButtonClick = this.handleSaveButtonClick.bind(this);
    }

    addClass() {
        this.setState(prevState => ({
            classes: [...prevState.classes, { className: '', classType: 'Regular', grade: 'A+' }]
        }));
    }

    removeClass(index) {
        this.setState(prevState => {
            const classes = [...prevState.classes];
            classes.splice(index, 1);
            return { classes };
        });
    }

    handleInputChange(event, index) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState(prevState => {
            const classes = [...prevState.classes];
            classes[index][name] = value;
            return { classes };
        });
    }

    handleStudentIDChange = (event) => {
        this.setState({ studentID: event.target.value });
    }

    calculateGPA() {
        const gradePoints = {
            'A+': 4.0, 'A': 4.0, 'A-': 3.7,
            'B+': 3.3, 'B': 3.0, 'B-': 2.7,
            'C+': 2.3, 'C': 2.0, 'C-': 1.7,
            'D+': 1.3, 'D': 1.0, 'D-': 0.7,
            'F+': 0.3, 'F': 0.0, 'F-': 0.0
        };

        const classTypes = {
            'Regular': 0,
            'Honors': 0.5,
            'AP': 1.0,
            'DE': 1.0
        };

        let totalGradePoints = 0;
        let totalWeightedGradePoints = 0;
        let totalClasses = 0;
        
        this.state.classes.forEach(classItem => {
            if (classItem.grade in gradePoints && classItem.classType in classTypes) {
                totalGradePoints += gradePoints[classItem.grade];
                totalWeightedGradePoints += gradePoints[classItem.grade] + classTypes[classItem.classType];
                totalClasses += 1;
            }
        });

        if (totalClasses > 0) {
            const unweightedGPA = totalGradePoints / totalClasses;
            const weightedGPA = totalWeightedGradePoints / totalClasses;

            this.setState({ unweightedGPA, weightedGPA });
        } else {
            this.setState({ unweightedGPA: null, weightedGPA: null });
        }
    }

    async fetchData() {
        const response = await fetch('gpacontext', { method: 'GET' });
        const data = await response.json();
        const filteredData = data.filter(item => item.studentID === this.state.studentID)
            .map(({ id, grade, ...rest }) => ({ grade: grade.trim(), ...rest })); // remove id and trim grade
        this.setState({ classes: filteredData });
        this.setState({ data: data });
    }

    async handleSaveButtonClick() {
        // Get the latest revision for the current student
        const response = await fetch('gpacontext', { method: 'GET' });
        const data = await response.json();
        const itemsForStudent = data.filter(item => item.studentID === this.state.studentID);
        const latestRevision = Math.max(...itemsForStudent.map(item => item.revision));

        // Prepare the data to be saved
        const classesToSave = this.state.classes.map((classItem) => ({
            ...classItem,
            studentID: this.state.studentID,
            revision: latestRevision + 1,
        }));

        // Post the data
        const saveResponse = await fetch('/gpacontext/Create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(classesToSave)
        });

        if (!saveResponse.ok) {
            // Handle the error
            console.error('Error saving data: ' + saveResponse.text());
        }
        else {
            console.log('Data saved successfully!');
        }
    }
    render() {
        return (
            <div>
                <table style={{ border: '1px solid black', borderCollapse: 'collapse' }}>
                    <thead>
                        <tr>
                            <th style={{ border: '1px solid black', padding: '10px' }}>Class Name</th>
                            <th style={{ border: '1px solid black', padding: '10px' }}>Class Type</th>
                            <th style={{ border: '1px solid black', padding: '10px' }}>Grade</th>
                            <th style={{ border: '1px solid black', padding: '10px' }}>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.classes.map((classItem, index) => (
                            <tr key={index}>
                                <td style={{ border: '1px solid black', padding: '10px' }}>
                                    <input type="text" name="className" value={classItem.className} onChange={(event) => this.handleInputChange(event, index)} style={{ width: '100%' }} />
                                </td>
                                <td style={{ border: '1px solid black', padding: '10px' }}>
                                    <select name="classType" value={classItem.classType} onChange={(event) => this.handleInputChange(event, index)} style={{ width: '100%', height: '100%' }}>
                                        <option value="Regular">Regular</option>
                                        <option value="Honors">Honors</option>
                                        <option value="AP">Advanced Placement</option>
                                        <option value="DE">Dual Enrollment</option>
                                    </select>
                                </td>
                                <td style={{ border: '1px solid black', padding: '10px' }}>
                                    <select name="grade" value={classItem.grade} onChange={(event) => this.handleInputChange(event, index)} style={{ width: '100%', height: '100%' }}>
                                        <option value="A+">A+</option>
                                        <option value="A">A</option>
                                        <option value="A-">A-</option>
                                        <option value="B+">B+</option>
                                        <option value="B">B</option>
                                        <option value="B-">B-</option>
                                        <option value="C+">C+</option>
                                        <option value="C">C</option>
                                        <option value="C-">C-</option>
                                        <option value="D+">D+</option>
                                        <option value="D">D</option>
                                        <option value="D-">D-</option>
                                        <option value="F+">F+</option>
                                        <option value="F">F</option>
                                        <option value="F-">F-</option>
                                    </select>
                                </td>
                                <td style={{ border: '1px solid black', padding: '10px' }}>
                                    <button onClick={() => this.removeClass(index)}>Remove Class</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                <button onClick={this.addClass}>Add Class</button>
                <button onClick={this.calculateGPA}>Calculate Total GPA</button>
                <p />
                <button onClick={this.fetchData}>Test</button>
                {this.state.unweightedGPA !== null && <p><strong>Unweighted GPA:</strong> {this.state.unweightedGPA.toFixed(2)}</p>}
                {this.state.weightedGPA !== null && <p><strong>Weighted GPA:</strong> {this.state.weightedGPA.toFixed(2)}</p>}
                <p />
                {this.state.data !== null && <p>{JSON.stringify(this.state.data)}</p>}
                <p>Current studentID:</p>
                <input
                    type="text"
                    value={this.state.studentID}
                    onChange={this.handleStudentIDChange}
                />
                <p />
                <button onClick={this.handleSaveButtonClick}>Save Data</button>
            </div>
        );
    }
}