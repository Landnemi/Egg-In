import { MapContainer, Marker, Popup, TileLayer} from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css";
import "leaflet-defaulticon-compatibility";
import InitialEvents from "../initialEvents/InitialEvents";
import { useState } from "react";
import { LatLng } from "leaflet";
import { signOut } from "next-auth/react"



const ProjectControls = (props) => {
  
    const {userData} = props;
    const [showDatasetForm, setShowDatasetForm] = useState(false);
    console.log(userData);



    return (
        <div style={{position: 'absolute', top: '10px', right:'10px', zIndex: '10000'}}> 
            <button onClick={()=>{setShowDatasetForm(!showDatasetForm)}}>Create Dataset</button>
            {showDatasetForm&&<div>asdasda</div>}
        </div>
    );
};

export default ProjectControls;