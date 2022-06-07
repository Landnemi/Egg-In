import { MapContainer, Marker, Popup, TileLayer} from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css";
import "leaflet-defaulticon-compatibility";
import InitialEvents from "../initialEvents/InitialEvents";
import { useState } from "react";
import { LatLng } from "leaflet";




const Map = (props) => {
  
  const {userData} = props;
  console.log(userData);
  const position = [33.43742900592779, -40.618167515754536]
  const [_userData, setUserData] = useState(userData)


  return (
    <> 
      <MapContainer
      center={[33.43742900592779, -40.618167515754536]}
      zoom={16}
      scrollWheelZoom={true}
      style={{ height: "100%", width: "100%"}}
      maxZoom={18}
      animate={false}
      doubleClickZoom={false}
      >
      

    

      <TileLayer
        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />
    {/* <Marker position={position}>
      <Popup>
        A pretty CSS3 popup. <br /> Easily customizable.
      </Popup>
    </Marker> */}

      {_userData.map((dataset, didx)=>{
        dataset.landmarks.map((landmark,lidx)=><Marker key={`${didx}-${lidx}`} position={position}  >
          <Popup>
            A pretty CSS3 popup. <br /> Easily customizable.
          </Popup>
          </Marker>
        )
      })}
      <InitialEvents name={props.name} email={props.email} userData={userData} />

      
    {/*       
       */}



    </MapContainer>   
  </>
  );
};

export default Map;