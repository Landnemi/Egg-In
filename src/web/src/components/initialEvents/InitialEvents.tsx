import { useState, useEffect, useRef, useMemo} from 'react'
import { Marker, Popup, useMap} from 'react-leaflet'
import Loading from '../loading/Loading'
import Form from '../form/BirdForm'
import { signOut } from "next-auth/react"


function InitialEvents(props) {
    const [position, setPosition] = useState(null);    
    const map = useMap();
    const markerRef = useRef(null)
    const [form, setForm] = useState(false);
    const popupElRef = useRef(null);


    const {userData} = props;



    useEffect(() => {
      map.locate().on("locationfound", function (e) {
        setPosition(e.latlng);
        map.flyTo(e.latlng, map.getZoom(), {animate: false});
      });
    }, []);

    const eventHandlers = useMemo(
      () => ({
        dragend() {
          const marker = markerRef.current
          //console.log(marker.getLatLng())
          if (marker != null) {
            setPosition(marker.getLatLng())
          }
        },
      }),
      [],
    )

    function handleClick() {
      setForm(true)
      // if (!popupElRef.current) return;
      // popupElRef.current._close();
      map.closePopup();
    }

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
          <Marker draggable='true' position={position} eventHandlers={eventHandlers} ref={markerRef}>
            <Popup ref={popupElRef}>
              <p>Create New Landmark</p>
                <button onClick={handleClick}>Create Landmark</button>
                <button onClick={() => signOut()}>Sign out</button>
              </Popup>
          </Marker>
        
        {datasets.map((dataset, didx)=>{
        return dataset.landmarks.map((landmark,lidx) => { return <Marker key={`${didx}-${lidx}`} position={[landmark.latitude, landmark.longitude]}  >
          <Popup>
            <p>id: {landmark.id}</p>
            <p>Dataset ID: {landmark.datasetId}</p>
            <p>Lat: {landmark.latitude}</p>
            <p>Lng: {landmark.longitude}</p>
            <p>Date created: {landmark.dateCreated}</p>
            <p>Status: { landmark.status}</p>
            <p>Progress: { landmark.progress}</p>
            <button onClick={handleClick}>Create Landmark</button>
            <button onClick={() => signOut()}>Sign out</button>
          </Popup>
        </Marker>
      })
      })}
          { form ? (<Form name={props.name} email={props.email} lat={position.lat} userData={userData} lng={position.lng} changeForm={form => setForm(form)}/>) : (<></>)}
        </div>
      );
  }

  export default InitialEvents