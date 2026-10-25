import {useEffect, useState } from 'react'
import * as Mui from '@mui/material'
import axios from 'axios';



type Activity = {
  id: string;
  title: string;
}

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
    axios.get<Activity[]>(`${import.meta.env.VITE_API_URL}/events`)
      .then(response => setActivities(response.data))

      return () => {};
    
  }, []);

  return (
    <>
      <Mui.Typography variant="h3" sx={{ color: 'blue' }}>
        Welcome to EventsHub
      </Mui.Typography>

      <Mui.List>
        {activities.map((activity: Activity) => (
          <Mui.ListItem key={activity.id}> 
            <Mui.ListItemText>{activity.title}</Mui.ListItemText>
            </Mui.ListItem>
        ))}
      </Mui.List>
    </>
  )
}

export default App
