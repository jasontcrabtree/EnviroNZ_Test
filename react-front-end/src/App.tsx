import { useEffect, useState } from "react";
import "./App.css";

type Suburb = {
  id: number;
  suburbName: string;
  latitude: number;
  longitude: number;
};

const API_ENDPOINT = `http://localhost:3001/api`;

function App() {
  const [suburbName, setSuburbName] = useState<string>("");

  const handleFetchSuburb = async (e: React.FormEvent) => {
    e.preventDefault();

    const formData = new FormData(e.target as HTMLFormElement);

    const response = await fetch(`API_ENDPOINT?latitude=${formData.get('latitude')}&longitude=${formData.get('longitude')}`, {
      method: "GET",
    })

    if (!response.ok) throw new Error("Failed to fetch suburb data")

    const suburb: Suburb = await response.json();

    setSuburbName(suburb.suburbName);
  }

  return (
    <div className="appWrapper">
      <form className="formWrapper" onSubmit={(e) => { handleFetchSuburb(e) }}>
        <label htmlFor="latitude">
          Latitude
          <input type="text" name="latitude" id="latitude" />
        </label>
        <label htmlFor="longitude">
          Longitude
          <input type="text" name="longitude" id="longitude" />
        </label>
        <input type="submit" value="Submit" />
      </form>
      {suburbName && <p>{suburbName}</p>}
    </div>
  );
}

export default App;
