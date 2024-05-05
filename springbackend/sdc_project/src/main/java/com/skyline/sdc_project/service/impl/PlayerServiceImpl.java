package com.skyline.sdc_project.service.impl;

import com.google.gson.JsonSyntaxException;
import com.skyline.sdc_project.dto.PlayerDTO;
import com.skyline.sdc_project.entity.Player;
import com.skyline.sdc_project.exception.UserNotFoundException;
import com.skyline.sdc_project.repository.PlayerRepo;
import com.skyline.sdc_project.service.PlayerService;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Component;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;

@Component
public class PlayerServiceImpl implements PlayerService {

    private final PlayerRepo repo;
    private final Gson gson;

    public PlayerServiceImpl(PlayerRepo repo, Gson gson) {
        this.repo = repo;
        this.gson = gson;
    }

    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        try {
            Player playerByUsername = repo.findAdminByUsername(username);
            if (playerByUsername != null) {
                List<String > list = gson.fromJson(playerByUsername.getType(), new TypeToken<ArrayList<String>>() {
                }.getType());
                String[] objects = (String[])list.toArray();
                return User.builder().username(
                        playerByUsername.getUsername()
                ).password(playerByUsername.getPassword()).roles(objects).build();
            }
            throw new UsernameNotFoundException("User not found");
        }catch (Exception e){
            throw new UsernameNotFoundException(e.getMessage(),e);
        }
    }


    @Override
    public PlayerDTO search(String username, String password) throws UserNotFoundException {
        try {
            Player playerByUsername = repo.findAdminByUsernameAndPassword(username, password);
            if (playerByUsername != null && playerByUsername.getPassword().equals(password) && playerByUsername.getUsername().equals(username)) {
                ArrayList<String> list = gson.fromJson(playerByUsername.getType(), new TypeToken<ArrayList<String>>() {}.getType());
                String[] objects = new String[list.size()];
                for (int i = 0; i < list.size(); i++) {
                    objects[i] = list.get(i);
                }
                return new PlayerDTO(playerByUsername.getId(), playerByUsername.getEmail(), playerByUsername.getUsername(), playerByUsername.getPassword(), list);
            }
            throw new UserNotFoundException("User not found");
        } catch (JsonSyntaxException e) {
            throw new UserNotFoundException("Invalid JSON format", e);
        } catch (Exception e) {
            throw new UserNotFoundException(e.getMessage(), e);
        }
    }

}
