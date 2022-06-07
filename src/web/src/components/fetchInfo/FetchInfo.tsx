
import useSWR from 'swr'
import axios from 'axios'


const fetcher = (url) => axios.get(url).then(response => response.data)

export default function UsersComponent(props) {

    const {userData} = props;
    // const {data, error} = useSWR(`http://fuglari.is:5000/datasets/${props.email}`, fetcher, {})
    // console.log(data)

return (
    <div>
        :)   
    </div>
)
}

