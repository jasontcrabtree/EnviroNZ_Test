import "./App.css";

const API_ENDPOINT = `http://localhost:3001/api`;

function App() {

  const handleForm = async (e: React.FormEvent) => {
    e.preventDefault();

    const formData = new FormData(e.target as HTMLFormElement);
    console.log('formData', formData.get('latitude'), formData.get('longitude'));
    const latitude = (e.target as HTMLFormElement).latitude.value;
    const longitude = (e.target as HTMLFormElement).longitude.value;

    const data = await fetch(`API_ENDPOINT?latitude=${latitude}&longitude=${longitude}`, {
      method: "GET",
    })

    return data;
  }

  return (
    <div>
      <form onSubmit={(e) => { handleForm(e) }}>
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
    </div>
  );
}

export default App;
