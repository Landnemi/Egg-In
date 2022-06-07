
import useSWR from 'swr'
import axios from 'axios'


const fetcher = (url) => axios.get(url).then(response => response.data)

export default function UsersComponent(props) {

    const {data, error} = useSWR(`http://fuglari.is:5000/datasets/${props.email}`, fetcher, {})
    console.log(data)
if (error) {
    return <p>{error.message}</p>
}

if (!data) {
    return <p>Loading...</p>
}

return (
    <div>
        {data.map((user) => (
            <p key={user.id}>{user.title}</p>
        ))}      
    </div>
)
}

