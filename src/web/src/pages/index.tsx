import dynamic from "next/dynamic";
import {signIn, signOut, useSession, getSession } from "next-auth/react"
import Link from "next/link";
// import useSWR from 'swr'
import axios from 'axios'


const fetcher = async (url) => axios.get(url).then(response => response.data)
export async function getServerSideProps(context) {
  // Fetch data from external API
  const session = await getSession(context);
  console.log(session);
  // this creates a new user with email if he does not exist. 
  const user_res = await fetch(`http://fuglari.is:5000/User/get_or_create/${session?.user?.email}`)
  // this gets a list of things that the user owns. 
  const res = await fetch(`http://fuglari.is:5000/datasets/${session?.user?.email}`)
  const data = await res.json()
  
  // const {data, error} = useSWR(`http://fuglari.is:5000/datasets/${props.email}`, fetcher, {})
  // Pass data to the page via props
  return { props: { data } }
}

export default function Home(props) {
  const MapWithNoSSR = dynamic(() => import("../components/map/Map"), {
    ssr: false,
  });

  const { data: session } = useSession()

  if(session) {
    return (
      <main>
        <div>
          <div id="map">
            <MapWithNoSSR name={session.user?.name} email={session.user?.email} userData={props.data}/>
          </div>
        </div>
      </main>
  )}

  return (
    <div className="login">
      <h1>Welcome to fuglari.is</h1>
      <button onClick={() => signIn()}>Sign in</button>
    </div>
  )
}