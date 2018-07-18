﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.Shared.DTO;
using Newtonsoft.Json;
using System.Linq;
using System.IO;

namespace AirportRESRfulApi.BLL.Services
{
    public class CrewsRemoteService : ICrewsRemoteService
    {
        private ICrewsService _crewsService;
        private IPilotsService _pilotsService;
        private IStewardessesService _stewardessesService;
        public CrewsRemoteService(ICrewsService crewsService, IPilotsService pilotsService, IStewardessesService stewardessesService)
        {
            _crewsService = crewsService;
            _pilotsService = pilotsService;
            _stewardessesService = stewardessesService;
        }


        private string url = "http://5b128555d50a5c0014ef1204.mockapi.io/crew";
        public async Task LoadCrews()
        {
            IEnumerable<CrewDto> crews = await GetRemoteCrewsAsync();
            var firstTenCrews = crews.Take(10);

            var tToDB = WriteCrewsToDbAsync(firstTenCrews);
            var tToLog = WriteCrewsToLogAsync(firstTenCrews);
            await Task.WhenAll(tToDB, tToLog);
        }

        private async Task WriteCrewsToDbAsync(IEnumerable<CrewDto> crews)
        {
            foreach(var crew in crews)
            {
                await _crewsService.AddAsync(crew);                                                                   
            }
        }

        private async Task WriteCrewsToLogAsync(IEnumerable<CrewDto> crews)
        {
            await Task.Run(() =>
            {
                using (StreamWriter writer = File.CreateText($"log_{DateTime.Now.ToString("MM-dd-yyyy_hh-mm-ss")}.csv"))
                {
                    writer.WriteLine($"RemoteId;DepartureId;PilotId;PilotFirstName;PilotLastName;PilotBirthDate;PilotExperience;" +
                        $"st1-Id;st1-FirstName;st1-LastName;st1-BirthDate;" +
                        $"st2-Id;st2-FirstName;st2-LastName;st2-BirthDate;");
                    foreach (var c in crews)
                    {

                        string line = $"{c.Id};{c.DepartureId};";

                        var p = c.Pilots?.FirstOrDefault();
                        if (p != null)
                            line += $"{p.Id};{p.FirstName};{p.LastName};{p.BirthDate};{p.Experience};";

                        for (int i = 0; i < c.Stewardesses.Count; i++)
                        {
                            StewardessDto s = c.Stewardesses[i];
                            line += $"{s.Id};{s.FirstName};{s.LastName};{s.BirthDate};";
                        }
                        writer.WriteLine(line);
                    }
                }
            });
        }

        private async Task<IEnumerable<CrewDto>> GetRemoteCrewsAsync()
        {
            string json = "";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    json = await client.GetStringAsync(url);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    json = "[{\"id\":\"1\",\"pilot\":[{\"id\":\"1\",\"crewId\":\"1\",\"birthDate\":\"2017-10-30T04:09:45.179Z\",\"firstName\":\"Kody\",\"lastName\":\"Oberbrunner\",\"exp\":26}],\"stewardess\":[{\"id\":\"1\",\"crewId\":\"1\",\"birthDate\":\"2018-07-04T20:20:33.347Z\",\"firstName\":\"Alyson\",\"lastName\":\"Koelpin\"},{\"id\":\"76\",\"crewId\":\"1\",\"birthDate\":\"2017-12-05T14:19:49.439Z\",\"firstName\":\"Sheila\",\"lastName\":\"Jacobs\"}]},{\"id\":\"2\",\"pilot\":[{\"id\":\"2\",\"crewId\":\"2\",\"birthDate\":\"2017-08-04T13:03:25.587Z\",\"firstName\":\"Carol\",\"lastName\":\"Howe\",\"exp\":41}],\"stewardess\":[{\"id\":\"2\",\"crewId\":\"2\",\"birthDate\":\"2018-06-17T01:19:39.084Z\",\"firstName\":\"Bo\",\"lastName\":\"Kuhic\"},{\"id\":\"77\",\"crewId\":\"2\",\"birthDate\":\"2017-10-13T16:37:50.321Z\",\"firstName\":\"Ari\",\"lastName\":\"Adams\"}]},{\"id\":\"3\",\"pilot\":[{\"id\":\"3\",\"crewId\":\"3\",\"birthDate\":\"2018-05-14T07:01:44.491Z\",\"firstName\":\"Alena\",\"lastName\":\"Thompson\",\"exp\":76}],\"stewardess\":[{\"id\":\"3\",\"crewId\":\"3\",\"birthDate\":\"2017-10-15T09:24:58.904Z\",\"firstName\":\"Stefan\",\"lastName\":\"Goyette\"},{\"id\":\"78\",\"crewId\":\"3\",\"birthDate\":\"2018-02-21T23:54:30.284Z\",\"firstName\":\"Dejah\",\"lastName\":\"Homenick\"}]},{\"id\":\"4\",\"pilot\":[{\"id\":\"4\",\"crewId\":\"4\",\"birthDate\":\"2017-07-26T10:33:54.064Z\",\"firstName\":\"Fritz\",\"lastName\":\"Kling\",\"exp\":4}],\"stewardess\":[{\"id\":\"4\",\"crewId\":\"4\",\"birthDate\":\"2017-10-03T01:12:34.315Z\",\"firstName\":\"Una\",\"lastName\":\"Cole\"},{\"id\":\"79\",\"crewId\":\"4\",\"birthDate\":\"2018-04-25T10:22:21.942Z\",\"firstName\":\"Donnie\",\"lastName\":\"Sanford\"}]},{\"id\":\"5\",\"pilot\":[{\"id\":\"5\",\"crewId\":\"5\",\"birthDate\":\"2018-02-04T13:52:55.559Z\",\"firstName\":\"Marshall\",\"lastName\":\"Padberg\",\"exp\":3}],\"stewardess\":[{\"id\":\"5\",\"crewId\":\"5\",\"birthDate\":\"2018-02-01T10:27:28.284Z\",\"firstName\":\"Lia\",\"lastName\":\"Effertz\"},{\"id\":\"80\",\"crewId\":\"5\",\"birthDate\":\"2018-05-02T09:15:03.494Z\",\"firstName\":\"Annie\",\"lastName\":\"Howe\"}]},{\"id\":\"6\",\"pilot\":[{\"id\":\"6\",\"crewId\":\"6\",\"birthDate\":\"2017-10-08T16:42:12.057Z\",\"firstName\":\"Virgil\",\"lastName\":\"Davis\",\"exp\":14}],\"stewardess\":[{\"id\":\"6\",\"crewId\":\"6\",\"birthDate\":\"2017-12-10T08:35:42.249Z\",\"firstName\":\"Harmon\",\"lastName\":\"Yost\"}]},{\"id\":\"7\",\"pilot\":[{\"id\":\"7\",\"crewId\":\"7\",\"birthDate\":\"2018-04-10T18:50:52.884Z\",\"firstName\":\"Bartholome\",\"lastName\":\"Lockman\",\"exp\":23}],\"stewardess\":[{\"id\":\"7\",\"crewId\":\"7\",\"birthDate\":\"2017-11-06T17:23:52.418Z\",\"firstName\":\"Ellis\",\"lastName\":\"Adams\"}]},{\"id\":\"8\",\"pilot\":[{\"id\":\"8\",\"crewId\":\"8\",\"birthDate\":\"2018-07-03T17:14:57.465Z\",\"firstName\":\"Rodrigo\",\"lastName\":\"Flatley\",\"exp\":34}],\"stewardess\":[{\"id\":\"8\",\"crewId\":\"8\",\"birthDate\":\"2017-12-01T10:13:59.875Z\",\"firstName\":\"Lia\",\"lastName\":\"Monahan\"}]},{\"id\":\"9\",\"pilot\":[{\"id\":\"9\",\"crewId\":\"9\",\"birthDate\":\"2018-05-17T15:32:30.621Z\",\"firstName\":\"Pearl\",\"lastName\":\"Rippin\",\"exp\":57}],\"stewardess\":[{\"id\":\"9\",\"crewId\":\"9\",\"birthDate\":\"2017-11-20T02:06:52.468Z\",\"firstName\":\"Hans\",\"lastName\":\"Sipes\"}]},{\"id\":\"10\",\"pilot\":[{\"id\":\"10\",\"crewId\":\"10\",\"birthDate\":\"2018-02-08T07:38:51.968Z\",\"firstName\":\"Derek\",\"lastName\":\"Emmerich\",\"exp\":31}],\"stewardess\":[{\"id\":\"10\",\"crewId\":\"10\",\"birthDate\":\"2017-10-27T05:15:06.777Z\",\"firstName\":\"Maudie\",\"lastName\":\"Hilll\"}]},{\"id\":\"11\",\"pilot\":[{\"id\":\"11\",\"crewId\":\"11\",\"birthDate\":\"2018-07-11T16:06:26.766Z\",\"firstName\":\"Blanca\",\"lastName\":\"Torphy\",\"exp\":50}],\"stewardess\":[{\"id\":\"11\",\"crewId\":\"11\",\"birthDate\":\"2018-03-30T16:44:47.746Z\",\"firstName\":\"Belle\",\"lastName\":\"Goyette\"}]},{\"id\":\"12\",\"pilot\":[{\"id\":\"12\",\"crewId\":\"12\",\"birthDate\":\"2018-03-07T21:45:50.127Z\",\"firstName\":\"Filomena\",\"lastName\":\"Maggio\",\"exp\":4}],\"stewardess\":[{\"id\":\"12\",\"crewId\":\"12\",\"birthDate\":\"2018-06-30T14:36:40.903Z\",\"firstName\":\"Daisha\",\"lastName\":\"Abshire\"}]},{\"id\":\"13\",\"pilot\":[{\"id\":\"13\",\"crewId\":\"13\",\"birthDate\":\"2017-08-27T23:41:27.370Z\",\"firstName\":\"Joaquin\",\"lastName\":\"Waelchi\",\"exp\":43}],\"stewardess\":[{\"id\":\"13\",\"crewId\":\"13\",\"birthDate\":\"2018-03-27T09:37:34.057Z\",\"firstName\":\"Christa\",\"lastName\":\"Heaney\"}]},{\"id\":\"14\",\"pilot\":[{\"id\":\"14\",\"crewId\":\"14\",\"birthDate\":\"2017-12-14T08:24:43.620Z\",\"firstName\":\"Maia\",\"lastName\":\"Moen\",\"exp\":88}],\"stewardess\":[{\"id\":\"14\",\"crewId\":\"14\",\"birthDate\":\"2017-07-23T12:58:21.231Z\",\"firstName\":\"Lenore\",\"lastName\":\"Murphy\"}]},{\"id\":\"15\",\"pilot\":[{\"id\":\"15\",\"crewId\":\"15\",\"birthDate\":\"2018-01-07T20:10:11.564Z\",\"firstName\":\"Antonette\",\"lastName\":\"Kulas\",\"exp\":35}],\"stewardess\":[{\"id\":\"15\",\"crewId\":\"15\",\"birthDate\":\"2017-11-28T17:17:51.772Z\",\"firstName\":\"Mozell\",\"lastName\":\"Wyman\"}]},{\"id\":\"16\",\"pilot\":[{\"id\":\"16\",\"crewId\":\"16\",\"birthDate\":\"2017-09-08T00:10:27.152Z\",\"firstName\":\"Alphonso\",\"lastName\":\"Fadel\",\"exp\":44}],\"stewardess\":[{\"id\":\"16\",\"crewId\":\"16\",\"birthDate\":\"2017-08-28T02:51:05.192Z\",\"firstName\":\"Kraig\",\"lastName\":\"Ondricka\"}]},{\"id\":\"17\",\"pilot\":[{\"id\":\"17\",\"crewId\":\"17\",\"birthDate\":\"2017-09-27T04:24:00.409Z\",\"firstName\":\"Ramon\",\"lastName\":\"Willms\",\"exp\":83}],\"stewardess\":[{\"id\":\"17\",\"crewId\":\"17\",\"birthDate\":\"2017-10-03T07:22:10.136Z\",\"firstName\":\"Henderson\",\"lastName\":\"Lehner\"}]},{\"id\":\"18\",\"pilot\":[{\"id\":\"18\",\"crewId\":\"18\",\"birthDate\":\"2017-08-25T20:04:31.473Z\",\"firstName\":\"Bart\",\"lastName\":\"Fadel\",\"exp\":61}],\"stewardess\":[{\"id\":\"18\",\"crewId\":\"18\",\"birthDate\":\"2018-01-26T19:29:26.959Z\",\"firstName\":\"Arnulfo\",\"lastName\":\"Watsica\"}]},{\"id\":\"19\",\"pilot\":[{\"id\":\"19\",\"crewId\":\"19\",\"birthDate\":\"2018-05-03T15:59:27.728Z\",\"firstName\":\"Brandyn\",\"lastName\":\"Barrows\",\"exp\":34}],\"stewardess\":[{\"id\":\"19\",\"crewId\":\"19\",\"birthDate\":\"2017-09-19T11:56:21.596Z\",\"firstName\":\"Cassidy\",\"lastName\":\"Hoeger\"}]},{\"id\":\"20\",\"pilot\":[{\"id\":\"20\",\"crewId\":\"20\",\"birthDate\":\"2018-05-05T17:58:17.612Z\",\"firstName\":\"Kelsie\",\"lastName\":\"Hayes\",\"exp\":93}],\"stewardess\":[{\"id\":\"20\",\"crewId\":\"20\",\"birthDate\":\"2018-06-17T16:15:08.804Z\",\"firstName\":\"Lewis\",\"lastName\":\"O'Connell\"}]},{\"id\":\"21\",\"pilot\":[{\"id\":\"21\",\"crewId\":\"21\",\"birthDate\":\"2017-10-03T21:05:42.134Z\",\"firstName\":\"Jeremy\",\"lastName\":\"Carroll\",\"exp\":29}],\"stewardess\":[{\"id\":\"21\",\"crewId\":\"21\",\"birthDate\":\"2017-11-16T18:11:24.157Z\",\"firstName\":\"Alfredo\",\"lastName\":\"Torp\"}]},{\"id\":\"22\",\"pilot\":[{\"id\":\"22\",\"crewId\":\"22\",\"birthDate\":\"2018-04-07T21:15:22.363Z\",\"firstName\":\"Loyal\",\"lastName\":\"D'Amore\",\"exp\":16}],\"stewardess\":[{\"id\":\"22\",\"crewId\":\"22\",\"birthDate\":\"2018-01-15T17:07:21.746Z\",\"firstName\":\"Lilla\",\"lastName\":\"Stehr\"}]},{\"id\":\"23\",\"pilot\":[{\"id\":\"23\",\"crewId\":\"23\",\"birthDate\":\"2018-04-01T19:07:54.831Z\",\"firstName\":\"Jaylen\",\"lastName\":\"Collins\",\"exp\":17}],\"stewardess\":[{\"id\":\"23\",\"crewId\":\"23\",\"birthDate\":\"2018-03-11T16:44:57.910Z\",\"firstName\":\"Karl\",\"lastName\":\"Waelchi\"}]},{\"id\":\"24\",\"pilot\":[{\"id\":\"24\",\"crewId\":\"24\",\"birthDate\":\"2017-10-23T08:11:06.682Z\",\"firstName\":\"Rebekah\",\"lastName\":\"Fahey\",\"exp\":4}],\"stewardess\":[{\"id\":\"24\",\"crewId\":\"24\",\"birthDate\":\"2017-08-19T12:11:57.506Z\",\"firstName\":\"Aletha\",\"lastName\":\"Mitchell\"}]},{\"id\":\"25\",\"pilot\":[{\"id\":\"25\",\"crewId\":\"25\",\"birthDate\":\"2018-07-11T18:14:09.764Z\",\"firstName\":\"Estella\",\"lastName\":\"Botsford\",\"exp\":39}],\"stewardess\":[{\"id\":\"25\",\"crewId\":\"25\",\"birthDate\":\"2017-12-26T17:14:19.906Z\",\"firstName\":\"Claudia\",\"lastName\":\"Zemlak\"}]},{\"id\":\"26\",\"pilot\":[{\"id\":\"26\",\"crewId\":\"26\",\"birthDate\":\"2017-12-03T02:52:37.692Z\",\"firstName\":\"Joesph\",\"lastName\":\"Berge\",\"exp\":86}],\"stewardess\":[{\"id\":\"26\",\"crewId\":\"26\",\"birthDate\":\"2017-10-02T02:48:10.058Z\",\"firstName\":\"Theo\",\"lastName\":\"Beahan\"}]},{\"id\":\"27\",\"pilot\":[{\"id\":\"27\",\"crewId\":\"27\",\"birthDate\":\"2018-01-20T04:47:37.595Z\",\"firstName\":\"Fay\",\"lastName\":\"Rolfson\",\"exp\":76}],\"stewardess\":[{\"id\":\"27\",\"crewId\":\"27\",\"birthDate\":\"2017-09-30T09:23:49.254Z\",\"firstName\":\"Lulu\",\"lastName\":\"Ziemann\"}]},{\"id\":\"28\",\"pilot\":[{\"id\":\"28\",\"crewId\":\"28\",\"birthDate\":\"2018-03-15T23:40:11.521Z\",\"firstName\":\"Pierce\",\"lastName\":\"Metz\",\"exp\":67}],\"stewardess\":[{\"id\":\"28\",\"crewId\":\"28\",\"birthDate\":\"2018-01-09T23:52:14.977Z\",\"firstName\":\"Hazel\",\"lastName\":\"Nitzsche\"}]},{\"id\":\"29\",\"pilot\":[{\"id\":\"29\",\"crewId\":\"29\",\"birthDate\":\"2017-07-21T19:17:17.625Z\",\"firstName\":\"Jarod\",\"lastName\":\"Hagenes\",\"exp\":26}],\"stewardess\":[{\"id\":\"29\",\"crewId\":\"29\",\"birthDate\":\"2018-01-07T04:29:52.706Z\",\"firstName\":\"Justen\",\"lastName\":\"Koepp\"}]},{\"id\":\"30\",\"pilot\":[{\"id\":\"30\",\"crewId\":\"30\",\"birthDate\":\"2017-10-19T21:16:59.376Z\",\"firstName\":\"Thomas\",\"lastName\":\"Kuvalis\",\"exp\":87}],\"stewardess\":[{\"id\":\"30\",\"crewId\":\"30\",\"birthDate\":\"2017-10-11T15:18:01.444Z\",\"firstName\":\"Hubert\",\"lastName\":\"O'Kon\"}]},{\"id\":\"31\",\"pilot\":[{\"id\":\"31\",\"crewId\":\"31\",\"birthDate\":\"2018-04-21T16:56:31.101Z\",\"firstName\":\"Jalen\",\"lastName\":\"Gerhold\",\"exp\":96}],\"stewardess\":[{\"id\":\"31\",\"crewId\":\"31\",\"birthDate\":\"2018-03-26T11:57:01.024Z\",\"firstName\":\"Jennifer\",\"lastName\":\"Waters\"}]},{\"id\":\"32\",\"pilot\":[{\"id\":\"32\",\"crewId\":\"32\",\"birthDate\":\"2018-05-25T21:52:09.586Z\",\"firstName\":\"Arnaldo\",\"lastName\":\"Kuvalis\",\"exp\":6}],\"stewardess\":[{\"id\":\"32\",\"crewId\":\"32\",\"birthDate\":\"2018-02-16T12:28:31.174Z\",\"firstName\":\"Elliot\",\"lastName\":\"Crist\"}]},{\"id\":\"33\",\"pilot\":[{\"id\":\"33\",\"crewId\":\"33\",\"birthDate\":\"2017-08-19T17:16:36.687Z\",\"firstName\":\"Remington\",\"lastName\":\"Vandervort\",\"exp\":10}],\"stewardess\":[{\"id\":\"33\",\"crewId\":\"33\",\"birthDate\":\"2017-11-16T13:33:58.626Z\",\"firstName\":\"Ottis\",\"lastName\":\"Schmeler\"}]},{\"id\":\"34\",\"pilot\":[{\"id\":\"34\",\"crewId\":\"34\",\"birthDate\":\"2017-08-28T16:16:27.161Z\",\"firstName\":\"Dion\",\"lastName\":\"Collier\",\"exp\":41}],\"stewardess\":[{\"id\":\"34\",\"crewId\":\"34\",\"birthDate\":\"2018-07-09T23:45:58.635Z\",\"firstName\":\"Oleta\",\"lastName\":\"Boyle\"}]},{\"id\":\"35\",\"pilot\":[{\"id\":\"35\",\"crewId\":\"35\",\"birthDate\":\"2017-10-27T11:07:33.723Z\",\"firstName\":\"Carissa\",\"lastName\":\"Bode\",\"exp\":67}],\"stewardess\":[{\"id\":\"35\",\"crewId\":\"35\",\"birthDate\":\"2017-08-16T13:32:25.673Z\",\"firstName\":\"Herminio\",\"lastName\":\"Schultz\"}]},{\"id\":\"36\",\"pilot\":[{\"id\":\"36\",\"crewId\":\"36\",\"birthDate\":\"2018-02-02T07:01:58.245Z\",\"firstName\":\"Kenny\",\"lastName\":\"Runte\",\"exp\":60}],\"stewardess\":[{\"id\":\"36\",\"crewId\":\"36\",\"birthDate\":\"2018-03-07T11:18:26.112Z\",\"firstName\":\"Fernando\",\"lastName\":\"Green\"}]},{\"id\":\"37\",\"pilot\":[{\"id\":\"37\",\"crewId\":\"37\",\"birthDate\":\"2017-12-03T18:20:46.103Z\",\"firstName\":\"Dion\",\"lastName\":\"Leannon\",\"exp\":13}],\"stewardess\":[{\"id\":\"37\",\"crewId\":\"37\",\"birthDate\":\"2018-04-08T03:17:56.534Z\",\"firstName\":\"Annie\",\"lastName\":\"Frami\"}]},{\"id\":\"38\",\"pilot\":[{\"id\":\"38\",\"crewId\":\"38\",\"birthDate\":\"2018-01-31T13:38:35.529Z\",\"firstName\":\"Monserrat\",\"lastName\":\"Cartwright\",\"exp\":76}],\"stewardess\":[{\"id\":\"38\",\"crewId\":\"38\",\"birthDate\":\"2018-05-25T09:55:27.235Z\",\"firstName\":\"Christina\",\"lastName\":\"Kirlin\"}]},{\"id\":\"39\",\"pilot\":[{\"id\":\"39\",\"crewId\":\"39\",\"birthDate\":\"2017-12-24T23:25:55.747Z\",\"firstName\":\"Jovan\",\"lastName\":\"Ernser\",\"exp\":27}],\"stewardess\":[{\"id\":\"39\",\"crewId\":\"39\",\"birthDate\":\"2017-11-30T23:50:12.941Z\",\"firstName\":\"Ted\",\"lastName\":\"Steuber\"}]},{\"id\":\"40\",\"pilot\":[{\"id\":\"40\",\"crewId\":\"40\",\"birthDate\":\"2018-01-18T12:04:48.567Z\",\"firstName\":\"Amari\",\"lastName\":\"Waelchi\",\"exp\":21}],\"stewardess\":[{\"id\":\"40\",\"crewId\":\"40\",\"birthDate\":\"2017-08-08T05:07:17.604Z\",\"firstName\":\"Geovanni\",\"lastName\":\"Hahn\"}]},{\"id\":\"41\",\"pilot\":[{\"id\":\"41\",\"crewId\":\"41\",\"birthDate\":\"2018-01-25T17:10:14.584Z\",\"firstName\":\"Carmine\",\"lastName\":\"Williamson\",\"exp\":58}],\"stewardess\":[{\"id\":\"41\",\"crewId\":\"41\",\"birthDate\":\"2017-12-29T16:34:37.947Z\",\"firstName\":\"Adela\",\"lastName\":\"Schultz\"}]},{\"id\":\"42\",\"pilot\":[{\"id\":\"42\",\"crewId\":\"42\",\"birthDate\":\"2018-02-26T13:18:26.663Z\",\"firstName\":\"Doug\",\"lastName\":\"Kilback\",\"exp\":37}],\"stewardess\":[{\"id\":\"42\",\"crewId\":\"42\",\"birthDate\":\"2018-07-12T19:52:56.355Z\",\"firstName\":\"Chelsea\",\"lastName\":\"Breitenberg\"}]},{\"id\":\"43\",\"pilot\":[{\"id\":\"43\",\"crewId\":\"43\",\"birthDate\":\"2018-04-18T03:49:55.296Z\",\"firstName\":\"Victoria\",\"lastName\":\"Harber\",\"exp\":17}],\"stewardess\":[{\"id\":\"43\",\"crewId\":\"43\",\"birthDate\":\"2017-11-04T06:53:42.659Z\",\"firstName\":\"Anahi\",\"lastName\":\"Wisozk\"}]},{\"id\":\"44\",\"pilot\":[{\"id\":\"44\",\"crewId\":\"44\",\"birthDate\":\"2018-01-26T22:51:02.211Z\",\"firstName\":\"Russel\",\"lastName\":\"Gorczany\",\"exp\":67}],\"stewardess\":[{\"id\":\"44\",\"crewId\":\"44\",\"birthDate\":\"2018-01-09T12:57:19.943Z\",\"firstName\":\"Myrtle\",\"lastName\":\"Heathcote\"}]},{\"id\":\"45\",\"pilot\":[{\"id\":\"45\",\"crewId\":\"45\",\"birthDate\":\"2017-07-19T20:13:03.259Z\",\"firstName\":\"Mara\",\"lastName\":\"O'Reilly\",\"exp\":23}],\"stewardess\":[{\"id\":\"45\",\"crewId\":\"45\",\"birthDate\":\"2018-04-09T03:25:58.631Z\",\"firstName\":\"Lucie\",\"lastName\":\"Becker\"}]},{\"id\":\"46\",\"pilot\":[{\"id\":\"46\",\"crewId\":\"46\",\"birthDate\":\"2018-01-23T12:41:06.048Z\",\"firstName\":\"Mohammad\",\"lastName\":\"Wisozk\",\"exp\":9}],\"stewardess\":[{\"id\":\"46\",\"crewId\":\"46\",\"birthDate\":\"2017-07-23T12:52:48.638Z\",\"firstName\":\"Hobart\",\"lastName\":\"Herman\"}]},{\"id\":\"47\",\"pilot\":[{\"id\":\"47\",\"crewId\":\"47\",\"birthDate\":\"2017-08-25T16:43:29.307Z\",\"firstName\":\"Christine\",\"lastName\":\"Hoeger\",\"exp\":83}],\"stewardess\":[{\"id\":\"47\",\"crewId\":\"47\",\"birthDate\":\"2017-12-01T09:55:40.941Z\",\"firstName\":\"Cleo\",\"lastName\":\"Kuvalis\"}]},{\"id\":\"48\",\"pilot\":[{\"id\":\"48\",\"crewId\":\"48\",\"birthDate\":\"2017-11-20T15:40:48.563Z\",\"firstName\":\"Elenor\",\"lastName\":\"Wiegand\",\"exp\":28}],\"stewardess\":[{\"id\":\"48\",\"crewId\":\"48\",\"birthDate\":\"2017-11-03T16:51:16.177Z\",\"firstName\":\"Magdalen\",\"lastName\":\"Rowe\"}]},{\"id\":\"49\",\"pilot\":[{\"id\":\"49\",\"crewId\":\"49\",\"birthDate\":\"2017-10-16T20:22:03.205Z\",\"firstName\":\"Beau\",\"lastName\":\"Sipes\",\"exp\":28}],\"stewardess\":[{\"id\":\"49\",\"crewId\":\"49\",\"birthDate\":\"2017-12-18T08:26:24.112Z\",\"firstName\":\"Rafaela\",\"lastName\":\"Mayert\"}]},{\"id\":\"50\",\"pilot\":[{\"id\":\"50\",\"crewId\":\"50\",\"birthDate\":\"2018-05-08T18:36:01.510Z\",\"firstName\":\"Deven\",\"lastName\":\"Mraz\",\"exp\":9}],\"stewardess\":[{\"id\":\"50\",\"crewId\":\"50\",\"birthDate\":\"2017-12-29T22:07:16.342Z\",\"firstName\":\"Darrick\",\"lastName\":\"O'Hara\"}]},{\"id\":\"51\",\"pilot\":[{\"id\":\"51\",\"crewId\":\"51\",\"birthDate\":\"2018-04-05T06:59:23.789Z\",\"firstName\":\"Fanny\",\"lastName\":\"Yundt\",\"exp\":85}],\"stewardess\":[{\"id\":\"51\",\"crewId\":\"51\",\"birthDate\":\"2018-06-18T13:59:22.148Z\",\"firstName\":\"Gonzalo\",\"lastName\":\"Erdman\"}]},{\"id\":\"52\",\"pilot\":[{\"id\":\"52\",\"crewId\":\"52\",\"birthDate\":\"2017-10-07T16:57:07.593Z\",\"firstName\":\"Krystina\",\"lastName\":\"O'Keefe\",\"exp\":22}],\"stewardess\":[{\"id\":\"52\",\"crewId\":\"52\",\"birthDate\":\"2017-07-18T21:35:16.096Z\",\"firstName\":\"Carleton\",\"lastName\":\"Wisozk\"}]},{\"id\":\"53\",\"pilot\":[{\"id\":\"53\",\"crewId\":\"53\",\"birthDate\":\"2017-07-30T03:11:42.541Z\",\"firstName\":\"Angelita\",\"lastName\":\"Kozey\",\"exp\":34}],\"stewardess\":[{\"id\":\"53\",\"crewId\":\"53\",\"birthDate\":\"2018-04-08T10:59:50.289Z\",\"firstName\":\"Trenton\",\"lastName\":\"Jacobi\"}]},{\"id\":\"54\",\"pilot\":[{\"id\":\"54\",\"crewId\":\"54\",\"birthDate\":\"2018-03-22T19:21:16.276Z\",\"firstName\":\"Vicenta\",\"lastName\":\"McDermott\",\"exp\":99}],\"stewardess\":[{\"id\":\"54\",\"crewId\":\"54\",\"birthDate\":\"2018-05-04T08:43:27.111Z\",\"firstName\":\"Wilhelmine\",\"lastName\":\"Leffler\"}]},{\"id\":\"55\",\"pilot\":[{\"id\":\"55\",\"crewId\":\"55\",\"birthDate\":\"2018-02-11T16:58:28.892Z\",\"firstName\":\"Dessie\",\"lastName\":\"Batz\",\"exp\":80}],\"stewardess\":[{\"id\":\"55\",\"crewId\":\"55\",\"birthDate\":\"2017-11-14T15:49:12.378Z\",\"firstName\":\"Flo\",\"lastName\":\"Roob\"}]},{\"id\":\"56\",\"pilot\":[{\"id\":\"56\",\"crewId\":\"56\",\"birthDate\":\"2018-03-23T17:59:16.218Z\",\"firstName\":\"Evalyn\",\"lastName\":\"Murphy\",\"exp\":86}],\"stewardess\":[{\"id\":\"56\",\"crewId\":\"56\",\"birthDate\":\"2018-02-14T03:15:27.160Z\",\"firstName\":\"Era\",\"lastName\":\"Cole\"}]},{\"id\":\"57\",\"pilot\":[{\"id\":\"57\",\"crewId\":\"57\",\"birthDate\":\"2017-10-09T21:55:31.019Z\",\"firstName\":\"Hadley\",\"lastName\":\"Cartwright\",\"exp\":76}],\"stewardess\":[{\"id\":\"57\",\"crewId\":\"57\",\"birthDate\":\"2018-06-21T12:47:27.365Z\",\"firstName\":\"Ofelia\",\"lastName\":\"Oberbrunner\"}]},{\"id\":\"58\",\"pilot\":[{\"id\":\"58\",\"crewId\":\"58\",\"birthDate\":\"2017-11-28T18:36:28.469Z\",\"firstName\":\"Nova\",\"lastName\":\"Grimes\",\"exp\":74}],\"stewardess\":[{\"id\":\"58\",\"crewId\":\"58\",\"birthDate\":\"2017-11-18T00:55:41.269Z\",\"firstName\":\"Alda\",\"lastName\":\"Wunsch\"}]},{\"id\":\"59\",\"pilot\":[{\"id\":\"59\",\"crewId\":\"59\",\"birthDate\":\"2017-08-11T16:44:06.701Z\",\"firstName\":\"Hollie\",\"lastName\":\"Lesch\",\"exp\":52}],\"stewardess\":[{\"id\":\"59\",\"crewId\":\"59\",\"birthDate\":\"2017-10-14T21:32:21.084Z\",\"firstName\":\"Josefina\",\"lastName\":\"Wiegand\"}]},{\"id\":\"60\",\"pilot\":[{\"id\":\"60\",\"crewId\":\"60\",\"birthDate\":\"2017-12-04T20:23:09.310Z\",\"firstName\":\"Raina\",\"lastName\":\"Ankunding\",\"exp\":93}],\"stewardess\":[{\"id\":\"60\",\"crewId\":\"60\",\"birthDate\":\"2018-05-12T05:49:36.447Z\",\"firstName\":\"Julia\",\"lastName\":\"Lubowitz\"}]},{\"id\":\"61\",\"pilot\":[{\"id\":\"61\",\"crewId\":\"61\",\"birthDate\":\"2017-11-02T04:14:07.426Z\",\"firstName\":\"Wilbert\",\"lastName\":\"Miller\",\"exp\":25}],\"stewardess\":[{\"id\":\"61\",\"crewId\":\"61\",\"birthDate\":\"2017-08-03T05:21:54.447Z\",\"firstName\":\"Jerad\",\"lastName\":\"Schneider\"}]},{\"id\":\"62\",\"pilot\":[{\"id\":\"62\",\"crewId\":\"62\",\"birthDate\":\"2018-03-09T13:56:11.240Z\",\"firstName\":\"Tess\",\"lastName\":\"Lemke\",\"exp\":69}],\"stewardess\":[{\"id\":\"62\",\"crewId\":\"62\",\"birthDate\":\"2018-03-10T05:17:02.587Z\",\"firstName\":\"Dana\",\"lastName\":\"Bartell\"}]},{\"id\":\"63\",\"pilot\":[{\"id\":\"63\",\"crewId\":\"63\",\"birthDate\":\"2018-03-11T04:43:25.813Z\",\"firstName\":\"Heaven\",\"lastName\":\"Cassin\",\"exp\":20}],\"stewardess\":[{\"id\":\"63\",\"crewId\":\"63\",\"birthDate\":\"2018-04-07T17:32:03.608Z\",\"firstName\":\"Andy\",\"lastName\":\"Ortiz\"}]},{\"id\":\"64\",\"pilot\":[{\"id\":\"64\",\"crewId\":\"64\",\"birthDate\":\"2017-12-02T07:37:22.429Z\",\"firstName\":\"Leola\",\"lastName\":\"Hoppe\",\"exp\":21}],\"stewardess\":[{\"id\":\"64\",\"crewId\":\"64\",\"birthDate\":\"2017-09-10T10:11:34.526Z\",\"firstName\":\"Camille\",\"lastName\":\"Boehm\"}]},{\"id\":\"65\",\"pilot\":[{\"id\":\"65\",\"crewId\":\"65\",\"birthDate\":\"2017-08-24T19:57:38.083Z\",\"firstName\":\"Fredrick\",\"lastName\":\"Bednar\",\"exp\":68}],\"stewardess\":[{\"id\":\"65\",\"crewId\":\"65\",\"birthDate\":\"2017-10-04T23:57:29.967Z\",\"firstName\":\"Giovanna\",\"lastName\":\"Nolan\"}]},{\"id\":\"66\",\"pilot\":[{\"id\":\"66\",\"crewId\":\"66\",\"birthDate\":\"2018-01-29T20:09:59.838Z\",\"firstName\":\"Martine\",\"lastName\":\"Kreiger\",\"exp\":44}],\"stewardess\":[{\"id\":\"66\",\"crewId\":\"66\",\"birthDate\":\"2017-10-22T05:28:53.210Z\",\"firstName\":\"Allene\",\"lastName\":\"Rath\"}]},{\"id\":\"67\",\"pilot\":[{\"id\":\"67\",\"crewId\":\"67\",\"birthDate\":\"2018-05-18T08:27:52.961Z\",\"firstName\":\"Sabryna\",\"lastName\":\"Jones\",\"exp\":48}],\"stewardess\":[{\"id\":\"67\",\"crewId\":\"67\",\"birthDate\":\"2018-06-21T02:53:52.922Z\",\"firstName\":\"Teresa\",\"lastName\":\"Yundt\"}]},{\"id\":\"68\",\"pilot\":[{\"id\":\"68\",\"crewId\":\"68\",\"birthDate\":\"2017-08-31T23:33:12.857Z\",\"firstName\":\"Yoshiko\",\"lastName\":\"Hilll\",\"exp\":47}],\"stewardess\":[{\"id\":\"68\",\"crewId\":\"68\",\"birthDate\":\"2018-02-22T14:30:39.998Z\",\"firstName\":\"Jaron\",\"lastName\":\"Bruen\"}]},{\"id\":\"69\",\"pilot\":[{\"id\":\"69\",\"crewId\":\"69\",\"birthDate\":\"2017-10-23T03:08:36.906Z\",\"firstName\":\"Karelle\",\"lastName\":\"Schimmel\",\"exp\":69}],\"stewardess\":[{\"id\":\"69\",\"crewId\":\"69\",\"birthDate\":\"2018-01-01T18:15:36.225Z\",\"firstName\":\"Abbie\",\"lastName\":\"Larson\"}]},{\"id\":\"70\",\"pilot\":[{\"id\":\"70\",\"crewId\":\"70\",\"birthDate\":\"2018-05-18T00:59:11.161Z\",\"firstName\":\"Wilhelmine\",\"lastName\":\"Runte\",\"exp\":14}],\"stewardess\":[{\"id\":\"70\",\"crewId\":\"70\",\"birthDate\":\"2018-03-29T07:10:50.876Z\",\"firstName\":\"Hellen\",\"lastName\":\"Marvin\"}]},{\"id\":\"71\",\"pilot\":[{\"id\":\"71\",\"crewId\":\"71\",\"birthDate\":\"2017-11-02T20:26:43.485Z\",\"firstName\":\"Dayna\",\"lastName\":\"Stanton\",\"exp\":9}],\"stewardess\":[{\"id\":\"71\",\"crewId\":\"71\",\"birthDate\":\"2017-12-06T19:28:41.425Z\",\"firstName\":\"Carol\",\"lastName\":\"Walsh\"}]},{\"id\":\"72\",\"pilot\":[{\"id\":\"72\",\"crewId\":\"72\",\"birthDate\":\"2018-02-15T02:26:54.589Z\",\"firstName\":\"Luz\",\"lastName\":\"Wuckert\",\"exp\":96}],\"stewardess\":[{\"id\":\"72\",\"crewId\":\"72\",\"birthDate\":\"2018-05-14T10:40:01.280Z\",\"firstName\":\"Gwen\",\"lastName\":\"Jacobs\"}]},{\"id\":\"73\",\"pilot\":[{\"id\":\"73\",\"crewId\":\"73\",\"birthDate\":\"2018-03-22T09:03:59.743Z\",\"firstName\":\"Clotilde\",\"lastName\":\"Rolfson\",\"exp\":19}],\"stewardess\":[{\"id\":\"73\",\"crewId\":\"73\",\"birthDate\":\"2017-09-25T08:52:07.709Z\",\"firstName\":\"Rory\",\"lastName\":\"Rogahn\"}]},{\"id\":\"74\",\"pilot\":[{\"id\":\"74\",\"crewId\":\"74\",\"birthDate\":\"2017-08-17T04:51:43.788Z\",\"firstName\":\"Cassandra\",\"lastName\":\"Welch\",\"exp\":23}],\"stewardess\":[{\"id\":\"74\",\"crewId\":\"74\",\"birthDate\":\"2017-10-31T13:00:43.093Z\",\"firstName\":\"Miracle\",\"lastName\":\"Torp\"}]},{\"id\":\"75\",\"pilot\":[{\"id\":\"75\",\"crewId\":\"75\",\"birthDate\":\"2018-03-02T18:24:50.527Z\",\"firstName\":\"Reilly\",\"lastName\":\"Braun\",\"exp\":67}],\"stewardess\":[{\"id\":\"75\",\"crewId\":\"75\",\"birthDate\":\"2018-05-01T12:54:37.810Z\",\"firstName\":\"Ardith\",\"lastName\":\"Johnson\"}]}]";
                }
            }
            return JsonConvert.DeserializeObject<List<CrewDto>>(json);
        }
    }
}
