//package com.skyline.sdc_project.controller;
//
//import com.skyline.sdc_project.entity.Player;
//import com.skyline.sdc_project.dto.PlayerDTO;
//
//import com.skyline.sdc_project.dto.ResponseDTO;
//import com.skyline.sdc_project.service.PlayerService;
//import com.skyline.sdc_project.util.VarList;
//import org.springframework.beans.factory.annotation.Autowired;
//import org.springframework.http.HttpStatus;
//import org.springframework.http.ResponseEntity;
//import org.springframework.web.bind.annotation.*;
//
//import java.util.List;
//
//@RestController
////@CrossOrigin(origins = "http://localhost:5173") // Allow requests from localhost:5173
//@RequestMapping("api/v1/player")
//public class PlayerController {
//
//    @Autowired
//    private PlayerService playerService;
//
//    @Autowired
//    private ResponseDTO responseDTO;
//
//    @PostMapping(value = "/savePlayer")
//    public ResponseEntity savePlayer(@RequestBody PlayerDTO playerDTO){
//        try {
//            String res = playerService.savePlayer(playerDTO);
//            if (res.equals("00")){
//                responseDTO.setCode(VarList.RSP_SUCCESS);
//                responseDTO.setMessage("Success");
//                responseDTO.setContent(playerDTO);
//                return new ResponseEntity(responseDTO, HttpStatus.ACCEPTED);
//            } else if(res.equals("06")) {
//                responseDTO.setCode(VarList.RSP_DUPLICATED);
//                responseDTO.setMessage("Player Registered");
//                responseDTO.setContent(playerDTO);
//                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
//            } else {
//                responseDTO.setCode(VarList.RSP_FAIL);
//                responseDTO.setMessage("Error");
//                responseDTO.setContent(null);
//                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
//            }
//        } catch (Exception ex){
//            responseDTO.setCode(VarList.RSP_ERROR);
//            responseDTO.setMessage(ex.getMessage());
//            responseDTO.setContent(null);
//            return new ResponseEntity(responseDTO, HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }
//
//
//    @PutMapping(value = "/updatePlayer")
//    public ResponseEntity updatePlayer(@RequestBody PlayerDTO playerDTO){
//        try {
//            String res = playerService.updatePlayer(playerDTO);
//            if (res.equals("00")){
//                responseDTO.setCode(VarList.RSP_SUCCESS);
//                responseDTO.setMessage("Success");
//                responseDTO.setContent(playerDTO);
//                return new ResponseEntity(responseDTO, HttpStatus.ACCEPTED);
//            } else if(res.equals("01")) {
//                responseDTO.setCode(VarList.RSP_DUPLICATED);
//                responseDTO.setMessage("Not A Registered Player");
//                responseDTO.setContent(playerDTO);
//                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
//            } else {
//                responseDTO.setCode(VarList.RSP_FAIL);
//                responseDTO.setMessage("Error");
//                responseDTO.setContent(null);
//                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
//            }
//        } catch (Exception ex){
//            responseDTO.setCode(VarList.RSP_ERROR);
//            responseDTO.setMessage(ex.getMessage());
//            responseDTO.setContent(null);
//            return new ResponseEntity(responseDTO, HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }
//
//    @GetMapping("/getAllPlayers")
//    public ResponseEntity getAllPlayers(){
//        try {
//            List<PlayerDTO> playerDTOList = playerService.getAllPlayer();
//            responseDTO.setCode(VarList.RSP_SUCCESS);
//            responseDTO.setMessage("Success");
//            responseDTO.setContent(playerDTOList);
//            return new ResponseEntity(responseDTO, HttpStatus.ACCEPTED);
//        } catch (Exception ex){
//            responseDTO.setCode(VarList.RSP_ERROR);
//            responseDTO.setMessage(ex.getMessage());
//            responseDTO.setContent(null);
//            return new ResponseEntity(responseDTO, HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }
//
//    @GetMapping("/searchPlayer/{playerID}")
//    public ResponseEntity searchPlayer(@PathVariable int playerID){
//        try {
//            PlayerDTO playerDTO = playerService.searchPlayer(playerID);
//            if (playerDTO !=null) {
//                responseDTO.setCode(VarList.RSP_SUCCESS);
//                responseDTO.setMessage("Success");
//                responseDTO.setContent(playerDTO);
//                responseDTO.setAuthenticate("true");
//                return new ResponseEntity(responseDTO, HttpStatus.ACCEPTED);
//            } else {
//                responseDTO.setCode(VarList.RSP_NO_DATA_FOUND);
//                responseDTO.setMessage("No Player Available For this playerID");
//                responseDTO.setContent(null);
//                responseDTO.setAuthenticate("false");
//                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
//            }
//        } catch (Exception e) {
//            responseDTO.setCode(VarList.RSP_ERROR);
//            responseDTO.setMessage(e.getMessage());
//            responseDTO.setContent(e);
//            return new ResponseEntity(responseDTO, HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }
//
//    @DeleteMapping("/deletePlayer/{playerID}")
//    public ResponseEntity deletePlayer(@PathVariable int playerID){
//        try {
//            String res = playerService.deletePlayer(playerID);
//            if (res.equals("00")) {
//                responseDTO.setCode(VarList.RSP_SUCCESS);
//                responseDTO.setMessage("Success");
//                responseDTO.setContent(null);
//                return new ResponseEntity(responseDTO, HttpStatus.ACCEPTED);
//            } else {
//                responseDTO.setCode(VarList.RSP_NO_DATA_FOUND);
//                responseDTO.setMessage("No Player Available For this playerID");
//                responseDTO.setContent(null);
//                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
//            }
//        } catch (Exception e) {
//            responseDTO.setCode(VarList.RSP_ERROR);
//            responseDTO.setMessage(e.getMessage());
//            responseDTO.setContent(e);
//            return new ResponseEntity(responseDTO, HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }
//}
package com.skyline.sdc_project.controller;

import com.skyline.sdc_project.dto.PlayerAnswersDTO;
import com.skyline.sdc_project.dto.PlayerDTO;
import com.skyline.sdc_project.dto.ResponseDTO;

import com.skyline.sdc_project.entity.Usercredentials;
import com.skyline.sdc_project.service.PlayerServiceManagement;
import com.skyline.sdc_project.util.VarList;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
//@CrossOrigin(origins = "http://localhost:5173") // Allow requests from localhost:5173
@CrossOrigin(origins = "*")
@RequestMapping("api/v1/player")
public class PlayerController {

    @Autowired
    private PlayerServiceManagement playerServiceManagement;

    @Autowired
    private ResponseDTO responseDTO;
    @PostMapping("/send_credentials")
    public void login(@RequestBody Usercredentials credentials) {
        System.out.println("Received login request");
        System.out.println("Username: " + credentials.getUsername());
        System.out.println("Password: " + credentials.getPassword());
    }

    @PostMapping(value = "/savePlayer")
    public ResponseEntity savePlayer(@RequestBody PlayerDTO playerDTO) {
        try {
            String res = playerServiceManagement.savePlayer(playerDTO);
            if (res.equals("00")) {
                responseDTO.setCode(VarList.RSP_SUCCESS);
                responseDTO.setMessage("Success");
                responseDTO.setContent(playerDTO);
                return new ResponseEntity(responseDTO, HttpStatus.ACCEPTED);
            } else if (res.equals("06")) {
                responseDTO.setCode(VarList.RSP_DUPLICATED);
                responseDTO.setMessage("Player Registered");
                responseDTO.setContent(playerDTO);
                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
            } else {
                responseDTO.setCode(VarList.RSP_FAIL);
                responseDTO.setMessage("Error");
                responseDTO.setContent(null);
                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
            }
        } catch (Exception ex) {
            responseDTO.setCode(VarList.RSP_ERROR);
            responseDTO.setMessage(ex.getMessage());
            responseDTO.setContent(null);
            return new ResponseEntity(responseDTO, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
    @PostMapping(value="/saveAnswers")
    public ResponseEntity savePlayerAnswers(@RequestBody PlayerAnswersDTO playerAnswersDTO) {
        try {
            String res = playerServiceManagement.savePlayerAnswers(playerAnswersDTO);
            if (res.equals("00")) {
                responseDTO.setCode(VarList.RSP_SUCCESS);
                responseDTO.setMessage("Success");
                responseDTO.setContent(playerAnswersDTO);
                return new ResponseEntity(responseDTO, HttpStatus.ACCEPTED);
            } else if (res.equals("06")) {
                responseDTO.setCode(VarList.RSP_DUPLICATED);
                responseDTO.setMessage("Player Registered");
                responseDTO.setContent(playerAnswersDTO);
                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
            } else {
                responseDTO.setCode(VarList.RSP_FAIL);
                responseDTO.setMessage("Error");
                responseDTO.setContent(null);
                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
            }
        } catch (Exception ex) {
            responseDTO.setCode(VarList.RSP_ERROR);
            responseDTO.setMessage(ex.getMessage());
            responseDTO.setContent(null);
            return new ResponseEntity(responseDTO, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
    @GetMapping(value="/answer/{id}")
    public ResponseEntity getPlayerAnswers(@PathVariable("id") Integer playerId) {
        try {
            PlayerAnswersDTO playerAnswersDTO = playerServiceManagement.getPlayerAnswers(playerId);
            if (playerAnswersDTO != null) {
                responseDTO.setCode(VarList.RSP_SUCCESS);
                responseDTO.setMessage("Success");
                responseDTO.setContent(playerAnswersDTO);
                return new ResponseEntity(responseDTO, HttpStatus.OK);
            } else {
                responseDTO.setCode(VarList.RSP_NO_DATA_FOUND);
                responseDTO.setMessage("Player Answers not found");
                responseDTO.setContent(null);
                return new ResponseEntity(responseDTO, HttpStatus.NOT_FOUND);
            }
        } catch (Exception ex) {
            responseDTO.setCode(VarList.RSP_ERROR);
            responseDTO.setMessage(ex.getMessage());
            responseDTO.setContent(null);
            return new ResponseEntity(responseDTO, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }




    @PutMapping(value = "/updatePlayer")
    public ResponseEntity updatePlayer(@RequestBody PlayerDTO playerDTO) {
        try {
            String res = playerServiceManagement.updatePlayer(playerDTO);
            if (res.equals("00")) {
                responseDTO.setCode(VarList.RSP_SUCCESS);
                responseDTO.setMessage("Success");
                responseDTO.setContent(playerDTO);
                return new ResponseEntity(responseDTO, HttpStatus.ACCEPTED);
            } else if (res.equals("01")) {
                responseDTO.setCode(VarList.RSP_DUPLICATED);
                responseDTO.setMessage("Not A Registered Player");
                responseDTO.setContent(playerDTO);
                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
            } else {
                responseDTO.setCode(VarList.RSP_FAIL);
                responseDTO.setMessage("Error");
                responseDTO.setContent(null);
                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
            }
        } catch (Exception ex) {
            responseDTO.setCode(VarList.RSP_ERROR);
            responseDTO.setMessage(ex.getMessage());
            responseDTO.setContent(null);
            return new ResponseEntity(responseDTO, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @GetMapping("/getAllPlayers")
    public ResponseEntity getAllPlayers() {
        try {
            List<PlayerDTO> playerDTOList = playerServiceManagement.getAllPlayer();
            responseDTO.setCode(VarList.RSP_SUCCESS);
            responseDTO.setMessage("Success");
            responseDTO.setContent(playerDTOList);
            return new ResponseEntity(responseDTO, HttpStatus.ACCEPTED);
        } catch (Exception ex) {
            responseDTO.setCode(VarList.RSP_ERROR);
            responseDTO.setMessage(ex.getMessage());
            responseDTO.setContent(null);
            return new ResponseEntity(responseDTO, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @GetMapping("/searchPlayer/{playerID}")
    public ResponseEntity searchPlayer(@PathVariable int playerID) {
        try {
            PlayerDTO playerDTO = playerServiceManagement.searchPlayer(playerID);
            if (playerDTO != null) {
                responseDTO.setCode(VarList.RSP_SUCCESS);
                responseDTO.setMessage("Success");
                responseDTO.setContent(playerDTO);
                responseDTO.setAuthenticate("true");
                return new ResponseEntity(responseDTO, HttpStatus.ACCEPTED);
            } else {
                responseDTO.setCode(VarList.RSP_NO_DATA_FOUND);
                responseDTO.setMessage("No Player Available For this playerID");
                responseDTO.setContent(null);
                responseDTO.setAuthenticate("false");
                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
            }
        } catch (Exception e) {
            responseDTO.setCode(VarList.RSP_ERROR);
            responseDTO.setMessage(e.getMessage());
            responseDTO.setContent(e);
            return new ResponseEntity(responseDTO, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @DeleteMapping("/deletePlayer/{playerID}")
    public ResponseEntity deletePlayer(@PathVariable int playerID) {
        try {
            String res = playerServiceManagement.deletePlayer(playerID);
            if (res.equals("00")) {
                responseDTO.setCode(VarList.RSP_SUCCESS);
                responseDTO.setMessage("Success");
                responseDTO.setContent(null);
                return new ResponseEntity(responseDTO, HttpStatus.ACCEPTED);
            } else {
                responseDTO.setCode(VarList.RSP_NO_DATA_FOUND);
                responseDTO.setMessage("No Player Available For this playerID");
                responseDTO.setContent(null);
                return new ResponseEntity(responseDTO, HttpStatus.BAD_REQUEST);
            }
        } catch (Exception e) {
            responseDTO.setCode(VarList.RSP_ERROR);
            responseDTO.setMessage(e.getMessage());
            responseDTO.setContent(e);
            return new ResponseEntity(responseDTO, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}