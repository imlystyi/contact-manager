import { useEffect, useState } from "react";
import "./App.css";

function App() {
  const [contacts, setContacts] = useState([]);

  useEffect(() => {
    getAllContacts();
  }, []);

  const deleteButtonClicked = async (id) => {
    await deleteContact(id);
    await getAllContacts();
  };

  const fileChange = async (event) => {
    const file = event.target.files[0];
    const fileContent = await readFileContent(file);
    await addContactCsv(fileContent);
    await getAllContacts();
  };

  const readFileContent = (file) => {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = (event) => {
        const contents = event.target.result;
        resolve(contents);
      };
      reader.onerror = (error) => {
        reject(error);
      };
      reader.readAsText(file);
    });
  };

  async function getAllContacts() {
    const response = await fetch("https://localhost:7071/api/get-all");
    const data = await response.json();
    setContacts(data);
  }

  async function addContactCsv(csv) {
    const response = await fetch("https://localhost:7071/api/add-csv", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(csv),
    });

    return response;
  }

  async function deleteContact(id) {
    const response = await fetch(`https://localhost:7071/api/delete/${id}`, {
      method: "DELETE",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    });

    return response;
  }

  return (
    <div className="app-container">
      <h1>Contact Manager</h1>
      <input type="file" onChange={fileChange} />
      <div className="content-container">
        {contacts.length === 0 ? (
          <p>
            <em>Loading...</em>
          </p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>ID</th>
                <th>Name</th>
                <th>Date of Birth</th>
                <th>Married</th>
                <th>Phone</th>
                <th>Salary</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              {contacts.map((contact) => (
                <tr key={contact.id}>
                  <td>{contact.id}</td>
                  <td>
                    <input
                      id="inputName"
                      className="inputName"
                      type="text"
                      placeholder={contact.name}
                    />
                  </td>
                  <td>
                    <input
                      className="inputBirthDate"
                      type="text"
                      placeholder={contact.birthDate}
                    />
                  </td>
                  <td>
                    <input
                      className="inputIsMarried"
                      type="text"
                      placeholder={contact.isMarried}
                    />
                  </td>
                  <td>
                    <input
                      className="inputPhone"
                      type="text"
                      placeholder={contact.phone}
                    />
                  </td>
                  <td>
                    <input
                      className="inputSalary"
                      type="text"
                      placeholder={contact.salary}
                    />
                  </td>
                  <td>
                    <button
                      className="btn btn-danger"
                      onClick={() => deleteButtonClicked(contact.id)}
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
}

export default App;
