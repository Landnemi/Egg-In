import Lottie from "lottie-web";
import { useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import axios from 'axios'
import { getSession } from "next-auth/react";



interface IDatasetForm {
    email: string;
    title: string;
    projectId: number;
}
function DatasetForm(props){
    const [close, setClose] = useState(true);
    function handleClick() {
        setClose(false)
    }
    const {
        register, 
        handleSubmit, 
        watch, 
        formState: {errors}} = useForm<IDatasetForm>();
  
        const datasetFormSubmitHandler: SubmitHandler<IDatasetForm> = (data: IDatasetForm) => {
            console.log("form data is", data)
            axios.post(
                'https://api.fuglari.is/api/Project/dataset',
                data,
                {headers: { 'Content-Type': 'application/json'}}
            )
            //   .then(response => window.location.reload())
            .catch(error => {console.log(error.data)})
        }


    return (
    <form onSubmit={handleSubmit(datasetFormSubmitHandler)} className="form">
    <label htmlFor="datasetId">Project Name *</label>
    <br/>
    <select {...register("projectId")}>
    { props.userData.map((project, index) => 
    <option key={index} value={project.id}>{project.title}</option>
    )}
    </select>
    <label htmlFor="latitude">Title, *</label>
    <br/>
    <input defaultValue={props.lat} {...register('title')}/>
    <input style={{ display:"none"}} defaultValue={props.email} {...register('email')}/>
    <input type="submit" onClick={() => handleClick()}/>
    <button type="close" onClick={() => props.changeForm(false)} >{close ? "Cancel" : "Close"}</button>
    </form>
  );
}
export default DatasetForm;