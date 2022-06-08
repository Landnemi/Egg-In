import { MapContainer, Marker, Popup, TileLayer} from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css";
import "leaflet-defaulticon-compatibility";
import { useState } from "react";
import DatasetForm from './DatasetForm'
import Select from 'react-select';



const ProjectControls = (props) => {
  
    const {userData} = props;
    const [showDatasetForm, setShowDatasetForm] = useState(false);
    const [selectedProject, setSelectedProject] = useState(null);
    console.log(userData);

    const handleChange = (e) => {
        console.log(e);
        
        setSelectedProject(e)
    }

    const selectOptions = userData.map(x=>{ return {'value':x.id, 'label':x.title} })

    return (
        <div style={{position: 'absolute', top: '10px', right:'10px', width:'200px', zIndex: '10000'}}> 

            <Select value={selectedProject} options={selectOptions} onChange={handleChange}/>
            
            <button onClick={()=>{setShowDatasetForm(!showDatasetForm)}}>Create Dataset</button>
            {showDatasetForm&&<div><DatasetForm userData={userData} email={props.email} /></div>}
            <button>Create Landmark</button> 
            
            
        </div>
    );
};

export default ProjectControls;