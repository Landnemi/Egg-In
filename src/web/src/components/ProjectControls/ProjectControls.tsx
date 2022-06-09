import { MapContainer, Marker, Popup, TileLayer, useMap} from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css";
import "leaflet-defaulticon-compatibility";
import { useState, useEffect, useRef, useMemo } from "react";
import DatasetForm from './DatasetForm'
import Select from 'react-select';
import Form from '../form/BirdForm'
import { signOut } from "next-auth/react"
import PatchForm from '../patchForm/PatchForm'



const ProjectControls = (props) => {
    const markerRef = useRef(null)
    const popupElRef = useRef(null);
    const {userData} = props;
    const map = useMap();
    
    const [showDatasetForm, setShowDatasetForm] = useState(false);
    const [selectedProject, setSelectedProject] = useState(null);
    const [isAddingLandmark, setIsAddingLandmark] = useState(false);
    console.log(userData);
    

    // useEffect(() => {
    //     map.locate().on("locationfound", function (e) {
    //       setPosition(e.latlng);
    //     });
    // }, []);
  
    function handleClick(e) {
        setForm(true)
        // if (!popupElRef.current) return;
        // popupElRef.current._close();
        // map.closePopup();
    }

    const [markerPosition, setMarkerPosition] = useState(map.getCenter());    

    
    const [form, setForm] = useState(false);


    const eventHandlers = useMemo(
        () => ({
          dragend() {
            const marker = markerRef.current
            //console.log(marker.getLatLng())
            if (marker != null) {
                setMarkerPosition(marker.getLatLng())
            }
          },
        }),
        [],
    )

    const handleStartMarking = () => {
        if(!isAddingLandmark){
            setMarkerPosition(map.getCenter())
        }
        setIsAddingLandmark(!isAddingLandmark);
    }

    const handleChange = (e) => {
        console.log(e);
        setSelectedProject(e)
    }

    const selectOptions = userData.map(x=>{ return {'value':x.id, 'label':x.title} })

    return (
        <div style={{position: 'absolute', top: '10px', right:'10px', width:'200px', zIndex: '10000'}}>             
            <button onClick={()=>{setShowDatasetForm(!showDatasetForm)}}>Create Dataset</button>
            {showDatasetForm&&<div><DatasetForm userData={userData} email={props.email} showDatasetForm={setShowDatasetForm} /></div>}
           
            <button onClick={handleStartMarking}>{isAddingLandmark? "Cancel creating Landmark" : "Create Landmark" }</button> 
            {isAddingLandmark  && <>
                <Marker draggable='true' position={markerPosition} eventHandlers={eventHandlers} ref={markerRef}>
                    <Popup ref={popupElRef}>
                        {form &&  <Form name={props.name} email={props.email} lat={markerPosition.lat} userData={userData} lng={markerPosition.lng} changeForm={form => setForm(form)}/> }
                        <button onClick={handleClick}>Create Landmark</button>
                    </Popup>
                </Marker>
            </>
            
            }
            
        </div>
    );
};

export default ProjectControls;