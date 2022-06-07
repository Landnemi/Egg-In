import useSWR from 'swr'
import axios from 'axios'


const fetcher = (url) => axios.get(url).then(response => response.data)

export default function UsersComponent() {

    const {data, error} = useSWR('https://jsonplaceholder.typicode.com/users', fetcher, {})

if (error) {
    <p>{error.message}</p>
}

if (!data) {
    <p>Loading...</p>
}

return (
    <div>
        {data.map(user => (
            <p key={user.id}>{user.name}</p>
        ))}
    </div>
)
}

