import { useState, useEffect, useRef, useMemo} from 'react'
import { Marker, Popup, useMap} from 'react-leaflet'
import Loading from '../loading/Loading'
import Form from '../form/BirdForm'
import { signOut } from "next-auth/react"
import PatchForm from '../patchForm/PatchForm'
import LandmarkMarker from './LandmarkMarker';
import ProjectControls from '../ProjectControls/ProjectControls'

function InitialEvents(props) {
    const map = useMap();

    
    const [position, setPosition] = useState(null);  
    useEffect(() => {
      map.locate().on("locationfound", function (e) {
        map.flyTo(e.latlng, map.getZoom(), {animate: false});
        setPosition(e.latlng);
      });
    }, []);

    const {userData} = props;

    const datasets = userData.map(x=>x.datasets).flat()
    console.log(datasets);
    
    useEffect(() => {
      map.locate().on("locationerror", function (e) {
        map.setView([64.1291137997281, -21.918854122890924]);
        //map.flyTo(map.unproject(position), map.getZoom(), {animate: false});
      });
    }, []);

    
    return position === null ? (
        <Loading/>     
      ) : (
        <div>
          {datasets.map((dataset, didx)=>{ return dataset.landmarks.map((landmark,lidx) => { console.log(landmark); return <LandmarkMarker key={`${didx}-${lidx}`} landmark={landmark} userData={userData} /> }) })}
          <ProjectControls userData={userData} email={props.email} />
        </div>
  );
}

export default InitialEvents