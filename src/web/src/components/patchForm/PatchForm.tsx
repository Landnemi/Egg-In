import Lottie from "lottie-web";
import { useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import axios from 'axios'



interface IBirdForm {
  id: number;  
  datasetId: number;
  latitude: string;
  longitude: string;
  dateCreated: string;
  status: string;
  progress: string;

}

function PatchForm(props) {

const [close, setClose] = useState(false);

function handleClick() {
  setClose(true)
}

const datasets = props.userData.map(x=>x.datasets).flat()

const {
  register, 
  handleSubmit, 
  watch, 
  formState: {errors}} = useForm<IBirdForm>();

const formSubmitHandler: SubmitHandler<IBirdForm> = (data: IBirdForm) => {
  console.log("form data is", data)
  axios
    .patch(
      'https://api.fuglari.is/api/Project/landmarks',
      data,
      {headers: { 'Content-Type': 'application/json'}}
    )
    .then(response => window.location.reload())
    .catch(error => {console.log(error.data)})
}
const birdForm = (
  <form onSubmit={handleSubmit(formSubmitHandler)} className="form">
    <label htmlFor="datasetId">Project Name *</label>
    <br/>
    <select {...register("datasetId")}>
      { datasets.map((dataset, index) => 
      <option key={index} value={dataset.id}>{dataset.title}</option>
     )}
    </select>
    <br/>
    <input defaultValue={props.id} {...register('id')}/>
    <br/>
    <br/>
    <label htmlFor="latitude">Lat, *</label>
    <br/>
    <input defaultValue={props.lat} {...register('latitude')}/>
    <br/>
    <br/>
    <label htmlFor="longitude">Lng, *</label>
    <br/>
    <input defaultValue={props.lng} {...register('longitude')}/>
    <br/>
    <br/>
    <label htmlFor="status">Status, *</label>
    <br/>
    <select {...register("status")}>
    <optgroup>
      <option value="1">Eggs</option>
      <option value="2">Hatched</option>
      <option value="3">Abandoned</option>
      </optgroup>
    </select>
    <br/>
    <br/>
    <label htmlFor="Progress">Progress, *</label>
    <br/>
    <select {...register("progress")}>
    <optgroup>
      <option value="1">In progress</option>
      <option value="2">Done</option>
      <option value="3">Cancelled</option>
      </optgroup>
    </select>
    <br/>
    <br/>
    <input type="submit" onClick={() => handleClick()}/>
    
  </form>
);

return (
    <div id='over'>
        <div className="box">
            { birdForm }
        </div>
    </div>
    )
}

export default PatchForm
