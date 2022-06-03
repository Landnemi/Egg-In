import { useState, useEffect, useRef} from 'react'
import { Marker, Popup, useMap} from 'react-leaflet'
import Loading from '../loading/Loading'

function InitialEvents() {
    const [position, setPosition] = useState(null);    
    const map = useMap();



    useEffect(() => {
      map.locate().on("locationfound", function (e) {
        setPosition(e.latlng);
        map.flyTo(e.latlng, map.getZoom(), {animate: false});
      });
    }, []);

    useEffect(() => {
      map.locate().on("locationerror", function (e) {
        map.setView([64.1291137997281, -21.918854122890924]);
        //map.flyTo(map.unproject(position), map.getZoom(), {animate: false});
      });
    }, []);
    return position === null ? (
      <Loading/>     
      ) : (
        <Marker position={position}>
          <Popup>{"Þú ert hér"}</Popup>
        </Marker>
      );
  }

  export default InitialEvents