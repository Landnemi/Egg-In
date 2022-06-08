import { useState, useEffect, useRef, useMemo} from 'react'
import { Marker, Popup, useMap} from 'react-leaflet'
import Loading from '../loading/Loading'
import Form from '../form/BirdForm'
import { signOut } from "next-auth/react"
import PatchForm from '../patchForm/PatchForm'


function LandmarkMarker(props) {
    const [position, setPosition] = useState(null);    
    const map = useMap();
    const markerRef = useRef(null)
    const [form, setForm] = useState(false);
    const popupElRef = useRef(null);

    const {landmark, userData, key} = props;


    const [isEditing, setIsEditing] = useState(false);


    function handleClick() {
      setForm(true)
      // if (!popupElRef.current) return;
      // popupElRef.current._close();
      map.closePopup();
    }


   
    

    return  (<Marker key={key} position={[landmark.latitude, landmark.longitude]}  >
        <Popup>
        <p>id: {landmark.id}</p>
        <p>Dataset ID: {landmark.datasetId}</p>
        <p>Lat: {landmark.latitude}</p>
        <p>Lng: {landmark.longitude}</p>
        <p>Date created: {landmark.dateCreated}</p>
        <p>Status: { landmark.status}</p>
        <p>Progress: { landmark.progress}</p>
        <button onClick={handleClick}>Edit Landmark</button>

        {isEditing && <PatchForm id={landmark.id} name={props.name} email={props.email} lat={position.lat} userData={userData} lng={position.lng} changeForm={form => setForm(form)}/> }
        </Popup>
    </Marker>)
  
  

  
}

export default LandmarkMarker