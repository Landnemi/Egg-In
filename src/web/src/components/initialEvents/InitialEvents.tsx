import { useState, useEffect, useRef, useMemo} from 'react'
import { Marker, Popup, useMap} from 'react-leaflet'
import Loading from '../loading/Loading'
import Form from '../form/BirdForm'


function InitialEvents(props) {
    const [position, setPosition] = useState(null);    
    const map = useMap();
    const markerRef = useRef(null)
    const [form, setForm] = useState(false);

    useEffect(() => {
      map.locate().on("locationfound", function (e) {
        setPosition(e.latlng);
        console.log("hallo", e.latlng)
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
    }

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
            <Popup> 
              {position.lat} {position.lng} <br/> {props.name} {props.email}
                <button onClick={handleClick}>
                  Create Landmark
                </button>
              </Popup>
          </Marker>

          { form ? (<Form name={props.name} email={props.email} lat={position.lat} lng={position.lng} changeForm={form => setForm(form)}/>) : (<></>)}
        </div>
      );
  }

  export default InitialEvents