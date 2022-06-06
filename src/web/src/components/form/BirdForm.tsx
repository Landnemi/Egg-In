import Lottie from "lottie-web";
import { useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";

interface IBirdForm {
  projectName: string;
  date: string;
  lat: string;
  lng: string;
  status: string;
  progress: string;

}

function Form(props) {

const [close, setClose] = useState(true);

function handleClick() {
  setClose(false)
}

const {
  register, 
  handleSubmit, 
  watch, 
  formState: {errors}} = useForm<IBirdForm>();

const formSubmitHandler: SubmitHandler<IBirdForm> = (data: IBirdForm) => {
  console.log("form data is", data)
}
const birdForm = (
  <form onSubmit={handleSubmit(formSubmitHandler)}>
    <label htmlFor="projectName">Project Name *</label>
    <input {...register('projectName')}/>
    <br/>
    <label htmlFor="date">Last updated, *</label>
    <input defaultValue={Date().toLocaleString()} {...register('date')}/>
    <br/>
    <label htmlFor="lat">Lat, *</label>
    <input defaultValue={props.lat} {...register('lat')}/>
    <br/>
    <label htmlFor="lng">Lng, *</label>
    <input defaultValue={props.lng} {...register('lng')}/>
    <br/>
    <label htmlFor="status">Status, *</label>
    <select {...register("status")}>
      <option value="Eggs">Eggs</option>
      <option value="Hatched">Hatched</option>
      <option value="Abandoned">Abandoned</option>
    </select>
    <br/>
    <label htmlFor="Progress">Progress, *</label>
    <select {...register("progress")}>
      <option value="In progress">In progress</option>
      <option value="Done">Done</option>
      <option value="Cancelled">Cancelled</option>
    </select>
    <input type="submit" onClick={() => handleClick()}/>
    <button type="close" onClick={() => props.changeForm(false)} >{close ? "Cancel" : "Close"}</button>
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

export default Form
