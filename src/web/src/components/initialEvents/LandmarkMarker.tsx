import { useState, useEffect, useRef, useMemo} from 'react'
import { Marker, Popup, useMap} from 'react-leaflet'
import Loading from '../loading/Loading'
import Form from '../form/BirdForm'
import { signOut } from "next-auth/react"
import PatchForm from '../patchForm/PatchForm'


function LandmarkMarker(props) { 
    const map = useMap();
    const {landmark, userData, key} = props;
    const [isEditing, setIsEditing] = useState(false);

    function handleClick() {
      setIsEditing(true)
      //if (!popupElRef.current) return;
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
        {isEditing ? <PatchForm id={landmark.id} name={props.name} email={props.email} lat={landmark.latitude} userData={userData} lng={landmark.longitude} changeEditing={isEditing => {setIsEditing(isEditing)}} /> : <></>}
        </Popup>
    </Marker>)
}

export default LandmarkMarker