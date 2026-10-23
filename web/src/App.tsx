import {useEffect, useState} from 'react'

type Activity = {
  id: string;
  title: string;
}

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
    fetch(`${import.meta.env.VITE_API_URL}/events`)
      .then((response) => response.json())
      .then(data => setActivities(data))

      return () => {};
    
  }, []);

  return (
    <div>
      <h3 style={{ color: 'blue' }}>Welcome to EventsHub</h3>
      <ul>
        {activities.map((activity) => (
          <li key={activity.id}>{activity.title}</li>
        ))}
      </ul>
    </div>
  )
}

export default App
