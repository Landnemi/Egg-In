import { MapContainer, TileLayer} from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css";
import "leaflet-defaulticon-compatibility";
import InitialEvents from "../initialEvents/InitialEvents";
import { useState } from "react";




const Map = (props) => {

  
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
      <InitialEvents name={props.name} email={props.email} />
    </MapContainer>   
  </>
  );
};

export default Map;